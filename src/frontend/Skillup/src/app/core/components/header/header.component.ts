import {
  Component,
  computed,
  inject,
  OnInit,
  signal,
  Signal,
  ViewChild,
} from '@angular/core';
import {
  NavigationEnd,
  Router,
  RouterLink,
  RouterLinkActive,
} from '@angular/router';
import { Drawer, DrawerModule } from 'primeng/drawer';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { AvatarModule } from 'primeng/avatar';
import { StyleClassModule } from 'primeng/styleclass';
import { LogoComponent } from '../../../utils/components/logo/logo.component';
import { FluidModule } from 'primeng/fluid';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { InputTextModule } from 'primeng/inputtext';
import { PopoverModule } from 'primeng/popover';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { UserService } from '../../../user/services/user.service';
import { User } from '../../../user/models/user.model';
import { AuthService } from '../../../auth/services/auth.service';
import { CartService } from '../../../finance/services/cart.service';
import { Cart } from '../../../finance/models/cart.model';
import { CourseItemShortComponent } from '../../../course/components/displays/course-item-short/course-item-short.component';
import { CoursesService } from '../../../course/services/course.service';
import { UserRole } from '../../../user/models/user-role.model';
import { MiniCartComponent } from "./mini-cart/mini-cart.component";
import { MiniCoursesComponent } from "./mini-courses/mini-courses.component";
import { MiniNotificationsComponent } from "./mini-notifications/mini-notifications.component";
import { NotificationService } from '../../../notifications/services/notification.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLinkActive,
    BadgeModule,
    OverlayBadgeModule,
    PopoverModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    LogoComponent,
    FluidModule,
    ButtonModule,
    RouterLink,
    DrawerModule,
    ButtonModule,
    RippleModule,
    AvatarModule,
    StyleClassModule,
    MiniCartComponent,
    MiniCoursesComponent,
    MiniNotificationsComponent,
    BadgeModule,
    OverlayBadgeModule 
],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  @ViewChild('drawerRef') drawerRef!: Drawer;
  router = inject(Router);
  userService = inject(UserService);
  authService = inject(AuthService);
  cartService = inject(CartService);
  courseService = inject(CoursesService);
  notificationService = inject(NotificationService);
  UserRole = UserRole;

  visible: boolean = false;
  user = signal<User | null>(null);
  notificationsNumber = this.notificationService.numberOfNotifications;


  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.visible = false;
      }
    });

    this.userService.user.subscribe((user) => {
      this.user.set(user);
    });
  }

  closeCallback(e: Event): void {
    this.drawerRef.close(e);
  }

  signOut() {
    this.authService.signOut().subscribe();
    this.visible = false;
  }

  
}
