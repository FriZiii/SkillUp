import { Component, inject, input, OnInit, signal } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User, UserDetail } from '../../models/user.model';
import { catchError, throwError } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-other-user-profile',
  standalone: true,
  imports: [ButtonModule, RouterModule],
  templateUrl: './other-user-profile.component.html',
  styleUrl: './other-user-profile.component.css'
})
export class OtherUserProfileComponent implements OnInit{
  //Variabled
  userId = input.required<string>();
  user = signal<UserDetail | null>(null);
  loggedUser = signal<User | null>(null);
  router = inject(Router);

  //Services
  userService = inject(UserService);

  ngOnInit(): void {
    this.userService.user.subscribe({
      next: (data) => {
        this.loggedUser.set(data);
    }});

    this.userService.getUser(this.userId())
      .pipe(
        catchError(error => { return throwError(() => error)}),
      )
      .subscribe({
        next: (data) => {
          this.user.set(data);
      }});
  }

  openWebsite(platform: 'website' | 'facebook' | 'twitter' | 'youTube' | 'linkedIn'){
    const baseUrls = {
      website: '',
      facebook: 'http://www.facebook.com/',
      twitter: 'https://x.com/',
      youTube: 'http://www.youtube.com/',
      linkedIn: 'http://www.linkedin.com/'
    };
    window.open(`${baseUrls[platform]}${this.user()?.socialMediaLinks[platform]}`, '_blank', 'noopener,noreferrer');
  }

  goToEdit(){
    this.router.navigate(['/user/edit/profile']);
  }
}
