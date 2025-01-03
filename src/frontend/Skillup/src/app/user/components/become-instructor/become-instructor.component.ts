import { Component, inject } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { UserService } from '../../services/user.service';
import { UserRole } from '../../models/user-role.model';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-become-instructor',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './become-instructor.component.html',
  styleUrl: './become-instructor.component.css'
})
export class BecomeInstructorComponent {
  authService = inject(AuthService);
  userService = inject(UserService);
  changeRole(){
    this.authService.changeUserRole(this.userService.currentUser()!.id, UserRole.Instructor).subscribe();
  }
}
