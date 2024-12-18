import { Component, inject, input } from '@angular/core';
import { Notification } from '../models/notification.model';
import { CommonModule } from '@angular/common';
import { BadgeModule } from 'primeng/badge';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-notification-item',
  standalone: true,
  imports: [CommonModule, BadgeModule],
  templateUrl: './notification-item.component.html',
  styleUrl: './notification-item.component.css'
})
export class NotificationItemComponent {
  notification = input.required<Notification>();
  isMini = input<boolean>(false);

  notificationService = inject(NotificationService);

  notificationClick(){
    this.notificationService.markNotificationAsSeen(this.notification().id).subscribe()
  }
}
