import { inject, Injectable, signal } from '@angular/core';
import {
  BehaviorSubject,
  filter,
  map,
  Observable,
  switchMap,
  take,
  tap,
} from 'rxjs';
import { EditUser, User, UserDetail } from '../models/user.model';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { UserRole } from '../models/user-role.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { WalletService } from '../../finance/services/wallet.service';
import { PurchasedItemsService } from '../../course/services/purchasedItems.service';
import { NotificationService } from '../../notifications/services/notification.service';

interface CustomJwtPayload extends JwtPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private httpClient = inject(HttpClient);
  private walletService = inject(WalletService);
  private purchasedItemsService = inject(PurchasedItemsService);
  private notificationService = inject(NotificationService);
  private userSubject = new BehaviorSubject<User | null>(null);
  private userDetailSubject = new BehaviorSubject<UserDetail | null>(null);
  currentUser = signal<User | null>(null); 

  get user(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  get userDetail(): Observable<UserDetail | null> {
    return this.userDetailSubject.asObservable();
  }

  setUserDetail(){
    this.getUserDetail().subscribe((res) => {
      this.userDetailSubject.next(res);
    })
  }

  private getUserDetail(): Observable<UserDetail | null> {
    return this.user.pipe(
      filter((user): user is User => user !== null),
      switchMap((user) =>
        this.getData(user.id , true).pipe(
          map((response: any) => {
            const userDetail = new UserDetail(user.id);
            userDetail.firstName = response.firstName;
            userDetail.lastName = response.lastName;
            userDetail.profilePicture = response.profilePicture;
            userDetail.email = response.email;
            userDetail.details.title = response.details.title;
            userDetail.details.biography = response.details.biography;
            userDetail.socialMediaLinks.website =
              response.socialMediaLinks.website;
            userDetail.socialMediaLinks.twitter =
              response.socialMediaLinks.twitter;
            userDetail.socialMediaLinks.facebook =
              response.socialMediaLinks.facebook;
            userDetail.socialMediaLinks.linkedIn =
              response.socialMediaLinks.linkedIn;
            userDetail.socialMediaLinks.youTube =
              response.socialMediaLinks.youTube;
            userDetail.privacySettings.isAccountPublicForLoggedInUsers =
              response.privacySettings.isAccountPublicForLoggedInUsers;
            userDetail.privacySettings.showCoursesOnUserProfile =
              response.privacySettings.showCoursesOnUserProfile;
            return userDetail;
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
      this.currentUser.set(user);
    });

    this.walletService.getWallet(user.id);
    this.purchasedItemsService.getPurchasedCourses(user.id);
    this.notificationService.getNotifications(user.id);
  }

  clearUser(): void {
    this.userSubject.next(null);
    this.currentUser.set(null);
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

  getUserWithoutDetail(userId: string) {
    return this.httpClient.get<User>(
      `${environment.apiUrl}/courses/users/${userId}?details=${false}`
    );
  }

  getUser(userId: string){
    return this.httpClient.get<UserDetail>(
      `${environment.apiUrl}/courses/users/${userId}?details=${true}`
    );
  }

  editUser(userId: string, userData:EditUser){
    return this.httpClient.put<any>(`${environment.apiUrl}/courses/users/${userId}`, userData)
    .pipe(
      tap((result: any) => {
        this.userDetailSubject.next(result);
      })
    )
    
  }

  editUserPrivacySettings(userId: string, publicAccount: boolean, visibleCourses: boolean){
    return this.httpClient.put<any>(`${environment.apiUrl}/courses/users/${userId}/Privacy-Settings`, {
      isAccountPublicForLoggedInUsers: publicAccount,
      showCoursesOnUserProfile: visibleCourses
    })
    .pipe(
      tap((result: any) => {
        this.userDetailSubject.next(result);
      })
    );
  }
}
