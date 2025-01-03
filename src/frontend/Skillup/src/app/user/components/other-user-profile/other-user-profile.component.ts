import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User, UserDetail } from '../../models/user.model';
import { ButtonModule } from 'primeng/button';
import { Router, RouterModule } from '@angular/router';
import { CoursesService } from '../../../course/services/course.service';
import { PurchasedItemsService } from '../../../course/services/purchasedItems.service';
import { CourseItemComponent } from "../../../course/components/displays/course-item/course-item.component";

@Component({
  selector: 'app-other-user-profile',
  standalone: true,
  imports: [ButtonModule, RouterModule, CourseItemComponent],
  templateUrl: './other-user-profile.component.html',
  styleUrl: './other-user-profile.component.css'
})
export class OtherUserProfileComponent implements OnInit{
  //Variables
  userId = input.required<string>();
  user = signal<UserDetail | null>(null);
  loggedUser = signal<User | null>(null);
  router = inject(Router);

  //Services
  userService = inject(UserService);
  courseService = inject(CoursesService);
  purchasedItemsService = inject(PurchasedItemsService)

  courses = computed(() => this.purchasedItemsService.purchasedCourses().map(c => this.courseService.mapCourseToCourseItem(c)));

  ngOnInit(): void {
    this.userService.user.subscribe({
      next: (data) => {
        this.loggedUser.set(data);
    }});

    this.userService.getUser(this.userId())
      .subscribe({
        next: (data) => {
          this.user.set(data);
      }});

      this.purchasedItemsService.getPurchasedCourses(this.userId());
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
