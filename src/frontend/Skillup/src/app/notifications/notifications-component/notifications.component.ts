import { Component, computed, inject } from '@angular/core';
import { NotificationService } from '../services/notification.service';
import { UserService } from '../../user/services/user.service';
import { UserRole } from '../../user/models/user-role.model';
import { NotificationItemComponent } from "../notification-item/notification-item.component";
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NotificationType } from '../models/notification.model';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [NotificationItemComponent, InputTextModule, FormsModule, CommonModule],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css'
})
export class NotificationsComponent {
  notificationService = inject(NotificationService);
  userService = inject(UserService);

  currentUser = this.userService.currentUser;
  notifications = this.notificationService.notifications;
  UserRole = UserRole;
  //userNotifications = computed(() => this.notifications().filter(n => n.type === NotificationType.User));
  //instructorNotifications = computed(() => this.notifications().filter(n => n.type === NotificationType.Instructor));
  numberOfUnseenNotofications = computed(() => this.notifications().filter(n => n.seen === false).length);

  //Filtering
  searchValue = '';
  sortDesc = true;
  seen = true;
  unseen = true;
  instructor = true;
  user = true;

  get filteredNotifications() {
    return this.notifications().filter(notification => {
      const matchesSearch = notification.message?.toLowerCase().includes(this.searchValue.toLowerCase());
      const matchesSeen = this.seen || !notification.seen;
      const matchesUnseen = this.unseen || notification.seen;
      const matchesInstructor = this.instructor || notification.type !== NotificationType.Instructor;
      const matchesUser = this.user || notification.type !== NotificationType.User;
      return matchesSearch && matchesSeen && matchesUnseen && matchesInstructor && matchesUser;
    }).sort((a, b) => {
      const dateA = new Date(a.createdAt).getTime();
      const dateB = new Date(b.createdAt).getTime();
      return this.sortDesc ? dateB - dateA : dateA - dateB;
    });
  }

  toggleSeen() {
    this.seen = !this.seen;
    if (!this.seen && !this.unseen) {
      this.unseen = true;
    }
  }
  
  toggleUnseen() {
    this.unseen = !this.unseen;
    if (!this.seen && !this.unseen) {
      this.seen = true;
    }
  }

  toggleInstructor() {
    this.instructor = !this.instructor;
    if (!this.instructor && !this.user) {
      this.user = true;
    }
  }
  
  toggleUser() {
    this.user = !this.user;
    if (!this.instructor && !this.user) {
      this.instructor = true;
    }
  }
}
