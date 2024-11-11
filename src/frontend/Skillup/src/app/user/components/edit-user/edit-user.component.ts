import { Component, inject, OnInit, signal } from '@angular/core';
import { TabsModule } from 'primeng/tabs';
import { RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { UserDetail } from '../../models/user.model';
import { EditUserProfileComponent } from "./edit-user-profile/edit-user-profile.component";

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [TabsModule, RouterModule, EditUserProfileComponent],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css',
})
export class EditUserComponent implements OnInit {
  userService = inject(UserService);
  userDetail = signal<UserDetail | null>(null);
  
  tabs = [
    {
      route: 'profile',
      label: 'Profile',
    },
    {
      route: 'profile-picture',
      label: 'Profile picture',
    },
    {
      route: 'privacy-settings',
      label: 'Privacy settings',
    },
  ];

  ngOnInit() {
    this.userService.setUserDetail();
  }
}
