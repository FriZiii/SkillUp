import { Component, inject, input, OnInit, signal } from '@angular/core';
import { UserService } from '../../../../user/services/user.service';
import { UserDetail } from '../../../../user/models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-description',
  standalone: true,
  imports: [],
  templateUrl: './author-description.component.html',
  styleUrl: './author-description.component.css'
})
export class AuthorDescriptionComponent implements OnInit {
  authorId = input.required<string>();

  //Services
  userService = inject(UserService);
  router = inject(Router);

  //Variables
  author = signal<UserDetail | null>(null);

  ngOnInit(): void {

    this.userService.getUser(this.authorId()).subscribe({
      next: (res) => {
        this.author.set(res);
      }
    });
  }

  navigateToAuthor() {
    this.router.navigate(['/user', this.author()?.id]);
  }
}
