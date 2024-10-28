import { BehaviorSubject, Observable, take, tap } from 'rxjs';
import { User } from '../models/user.model';
import { inject, Injectable } from '@angular/core';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { UserRole } from '../models/user-role.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

interface CustomJwtPayload extends JwtPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  httpClient = inject(HttpClient);
  private userSubject = new BehaviorSubject<User | null>(null);

  get user(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  setUserFromToken(token: string): void {
    const decodedToken = jwtDecode<CustomJwtPayload>(token);

    const user = new User(
      decodedToken.sub!,
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ]! as UserRole
    );

    this.getData(user.id, false).subscribe((response: any) => {
      user.firstName = response.firstName;
      user.lastName = response.lastName;
      user.profilePicture = response.profilePicture;
      this.userSubject.next(user);
    });
  }

  clearUser(): void {
    this.userSubject.next(null);
  }

  editProfilePicture(userId: string, file: File) {
    const formData = new FormData();
    formData.append('file', file);

    return this.httpClient
      .put<any>(
        `${environment.apiUrl}/courses/users/${userId}/profile-picture`,
        formData
      )
      .pipe(
        tap((result: any) => {
          this.userSubject.pipe(take(1)).subscribe((user) => {
            if (user) {
              user.profilePicture = result.profilePicture;
              this.userSubject.next(user);
            }
          });
        })
      );
  }

  private getData(userId: string, details: boolean) {
    return this.httpClient.get(
      `${environment.apiUrl}/courses/users/${userId}?details=${details}`
    );
  }
}
