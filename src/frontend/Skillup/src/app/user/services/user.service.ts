import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  //private jwtHelper = new JwtHelperService();
  private userSubject = new BehaviorSubject<User | null>(null);

  get user(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  setUserFromToken(token: string): void {
    const decodedToken = jwtDecode(token);
    // console.log(decodedToken);
    // const user: User = {
    //  id: decodedToken.sub,
    //  role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
    // };
    // this.userSubject.next(user);
  }

  clearUser(): void {
    this.userSubject.next(null);
  }
}
