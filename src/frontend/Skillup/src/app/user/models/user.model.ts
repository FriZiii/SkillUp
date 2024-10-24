import { UserRole } from './user-role.model';

export class User {
  id: string;
  role: UserRole;

  constructor(id: string, role: UserRole) {
    this.id = id;
    this.role = role;
  }
}
