import { HttpClient } from "@angular/common/http";
import { computed, inject, Injectable, signal } from "@angular/core";
import { environment } from "../../../environments/environment";
import { single, tap } from "rxjs";
import { Notification } from "../models/notification.model";

@Injectable({ providedIn: 'root' })
export class NotificationService {
    
  private httpClient = inject(HttpClient);
  notifications = signal<Notification[]>([]);
  numberOfNotifications = computed(() => this.notifications().filter(n => n.seen === false).length);
  
  public getNotifications(userId: string) {
      this.httpClient
        .get<Notification[]>(environment.apiUrl + '/Notifications/Users/' + userId, {})
        .pipe(
          tap((res) => {
            this.notifications.set(res);
            console.log(this.notifications());
          })
        ).subscribe();
    }

    public markNotificationAsSeen(notificationId: string){
        return this.httpClient
        .patch(environment.apiUrl + '/Notifications/' + notificationId, {})
        .pipe(
          tap((res) => {
            this.notifications.update(notifications =>
                notifications.map(n => n.id === notificationId ? { ...n, seen: true } : n)
              );
          })
        )
    }
}