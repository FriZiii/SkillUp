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
