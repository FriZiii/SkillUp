import { BehaviorSubject, catchError, filter, map, Observable, switchMap, tap, throwError } from 'rxjs';
import { EditUser, User, UserDetail } from '../models/user.model';
import { inject, Injectable, signal } from '@angular/core';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { UserRole } from '../models/user-role.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ToastHandlerService } from '../../core/services/toasthandler.service';

interface CustomJwtPayload extends JwtPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  toastService = inject(ToastHandlerService)
  httpClient = inject(HttpClient);
  private userSubject = new BehaviorSubject<User | null>(null);

  get user(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  get userDeatil(): Observable<UserDetail | null> {
    return this.user.pipe(
      filter((user): user is User => user !== null),
      switchMap((user) =>
        this.getData(user.id, true).pipe(
          map((response: any) => {
            const userDetail = new UserDetail(user.id);
            userDetail.firstName = response.firstName;
            userDetail.lastName = response.lastName;
            userDetail.profilePicture = response.profilePicture;
            userDetail.email = response.email;
            userDetail.details.title = response.details.title;
            userDetail.details.biography = response.details.biography;
            userDetail.socialMediaLinks.website = response.socialMediaLinks.website;
            userDetail.socialMediaLinks.twitter = response.socialMediaLinks.twitter;
            userDetail.socialMediaLinks.facebook = response.socialMediaLinks.facebook;
            userDetail.socialMediaLinks.linkedin = response.socialMediaLinks.linkedin;
            userDetail.socialMediaLinks.youtube = response.socialMediaLinks.youtube;
            userDetail.privacySettings.isAccountPublicForLoggedInUsers = response.privacySettings.isAccountPublicForLoggedInUsers;
            userDetail.privacySettings.showCoursesOnUserProfile = response.privacySettings.showCoursesOnUserProfile;
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

  editUser(userId: string, userData:EditUser){
    return this.httpClient.put<any>(`${environment.apiUrl}/courses/users/${userId}`, userData)
    .pipe(
      catchError(error => { return throwError(() => error)}),
    );
  }

}
