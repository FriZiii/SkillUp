import { Component, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
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

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
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
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  @ViewChild('drawerRef') drawerRef!: Drawer;

  closeCallback(e: Event): void {
    this.drawerRef.close(e);
  }

  visible: boolean = false;
}
