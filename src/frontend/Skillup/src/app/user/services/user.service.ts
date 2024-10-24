import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { Injectable } from '@angular/core';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { UserRole } from '../models/user-role.model';

interface CustomJwtPayload extends JwtPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private userSubject = new BehaviorSubject<User | null>(null);

  get user(): Observable<User | null> {
    return this.userSubject.asObservable();
  }

  setUserFromToken(token: string): void {
    const decodedToken = jwtDecode<CustomJwtPayload>(token);

    const user: User = {
      id: decodedToken.sub!,
      role: UserRole[
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ]! as keyof typeof UserRole
      ],
    };

    this.userSubject.next(user);
  }

  clearUser(): void {
    this.userSubject.next(null);
  }
}
