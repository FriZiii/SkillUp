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

  public isInRole(role: UserRole){
    return this.role === role;
  }
}

export class UserDetail {
  id: string;
  firstName: string | null = null;
  lastName: string | null = null;
  profilePicture: string | null = null;
  email: string | null = null;
  details: {
    title: string | null;
    biography: string | null;
  } = {title: null, biography: null}
  socialMediaLinks: {
    website: string | null;
    twitter: string | null;
    facebook: string | null;
    linkedIn: string | null;
    youTube: string | null;
  } = {website: null, twitter: null, facebook: null, linkedIn: null, youTube: null};
  privacySettings: {
  isAccountPublicForLoggedInUsers: boolean;
  showCoursesOnUserProfile: boolean;
  } = {isAccountPublicForLoggedInUsers: false, showCoursesOnUserProfile: false};


  constructor(id: string) {
    this.id = id;
  }
}

export interface EditUser{
  firstName: string | null;
  lastName: string | null;
  email: string | null;
  title: string | null;
  biography: string | null;
  socialMediaLinks: {
    twitter: string | null;
    facebook: string | null;
    website: string | null;
    linkedin: string | null;
    youtube: string | null;
  }
}
