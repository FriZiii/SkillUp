import { BehaviorSubject, filter, map, Observable, switchMap, tap } from 'rxjs';
import { User, UserDetail } from '../models/user.model';
import { inject, Injectable, signal } from '@angular/core';
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
  //private userDetailSubject = new BehaviorSubject<UserDetail | null>(null);

  get user(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  get userDeatil(): Observable<UserDetail | null> {
    return this.user.pipe(
      filter((user): user is User => user !== null), // Upewniamy się, że user nie jest null
      switchMap((user) =>
        this.getData(user.id, true).pipe(
          map((response: any) => {
            const userDetail = new UserDetail(user.id, user.role);
            userDetail.firstName = response.firstName;
            userDetail.lastName = response.lastName;
            userDetail.profilePicture = response.profilePicture;
            userDetail.email = response.email;
            userDetail.title = response.details.title;
            userDetail.biography = response.details.biography;
            userDetail.website = response.socialMediaLinks.website;
            userDetail.twitter = response.socialMediaLinks.twitter;
            userDetail.facebook = response.socialMediaLinks.facebook;
            userDetail.linkedin = response.socialMediaLinks.linkedin;
            userDetail.youtube = response.socialMediaLinks.youtube;
            userDetail.isAccountPublicForLoggedInUsers = response.privacySettings.isAccountPublicForLoggedInUsers;
            userDetail.showCoursesOnUserProfile = response.privacySettings.showCoursesOnUserProfile;
            return userDetail;  // Tutaj zwracamy skonstruowany obiekt userDetail
          })
        )
      )
    );
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

  private getData(userId: string, details: boolean) {
    return this.httpClient.get(
      `${environment.apiUrl}/courses/users/${userId}?details=${details}`
    );
  }
}
