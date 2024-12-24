import { Component, computed, inject } from '@angular/core';
import { NotificationService } from '../../../../notifications/services/notification.service';
import { UserService } from '../../../../user/services/user.service';
import { UserRole } from '../../../../user/models/user-role.model';
import { TabsModule } from 'primeng/tabs';
import { NotificationType } from '../../../../notifications/models/notification.model';
import { NotificationItemComponent } from "../../../../notifications/notification-item/notification-item.component";
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-mini-notifications',
  standalone: true,
  imports: [TabsModule, NotificationItemComponent, RouterModule],
  templateUrl: './mini-notifications.component.html',
  styleUrl: './mini-notifications.component.css'
})
export class MiniNotificationsComponent {
  notificationService = inject(NotificationService);
  userService = inject(UserService);

  user = this.userService.currentUser;
  notifications = this.notificationService.notifications;
  UserRole = UserRole;
  userNotifications = computed(() => this.notifications().filter(n => n.type === NotificationType.User));
  instructorNotifications = computed(() => this.notifications().filter(n => n.type === NotificationType.Instructor));
}
