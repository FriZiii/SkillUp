import { UserRole } from './user-role.model';

export class User {
  id: string;
  role: UserRole;
  firstName: string | null = null;
  lastName: string | null = null;
  profilePicture: string | null = null;

  constructor(id: string, role: UserRole) {
    this.id = id;
    this.role = role;
  }
}

export class UserDetail {
  id: string;
  role: UserRole;
  firstName: string | null = null;
  lastName: string | null = null;
  profilePicture: string | null = null;
  email: string | null = null;
  title: string | null = null;
  biography: string | null = null;
  website: string | null = null;
  twitter: string | null = null;
  facebook: string | null = null;
  linkedin: string | null = null;
  youtube: string | null = null;
  isAccountPublicForLoggedInUsers: boolean = false;
  showCoursesOnUserProfile: boolean = false;


  constructor(id: string, role: UserRole) {
    this.id = id;
    this.role = role;
  }
}
