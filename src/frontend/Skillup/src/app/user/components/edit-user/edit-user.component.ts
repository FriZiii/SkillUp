import { Component } from '@angular/core';
import { TabsModule } from 'primeng/tabs';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [TabsModule, RouterModule],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css',
})
export class EditUserComponent {
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
}
