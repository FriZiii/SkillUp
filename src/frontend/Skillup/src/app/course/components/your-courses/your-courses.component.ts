import { Component, inject, input, OnInit, signal } from '@angular/core';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PurchasedItemsService } from '../../services/purchasedItems.service';
import { CourseItemShortComponent } from '../displays/course-item-short/course-item-short.component';
import { CourseRatingService } from '../../services/course-rating.service';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { UserService } from '../../../user/services/user.service';
import { UserRating } from '../../models/rating.model';
import { UserRole } from '../../../user/models/user-role.model';

@Component({
  selector: 'app-your-courses',
  standalone: true,
  imports: [ProgressSpinnerModule, CourseItemShortComponent, DialogModule, ButtonModule, RatingModule, FormsModule, InputTextModule],
  templateUrl: './your-courses.component.html',
  styleUrl: './your-courses.component.css'
})
export class YourCoursesComponent implements OnInit {
   //From URL
   userId = input.required<string>();

   //Services
   courseService = inject(PurchasedItemsService);
   ratingService = inject(CourseRatingService);
   userService = inject(UserService);
  
  //Variables
  loading = true;
  courses = this.courseService.purchasedCourses;
  courseRatings: UserRating[] = []
  currentRating: UserRating | undefined = undefined;
  addRatingDialogVisible = false;
  newRating = 1;
  newFeedback = '';
  currentCourseId = '';

  ngOnInit(): void {
    this.loading = false;

    this.ratingService.getUserRatings(this.userService.currentUser()!.id).subscribe(
      (res) => this.courseRatings = res
    );
  }

  addRating(courseId: string){
    this.addRatingDialogVisible = true;
    this.currentCourseId = courseId;
    this.currentRating = this.courseRatings.find(r => r.courseId === courseId);
    if(this.currentRating){
      this.newRating = this.currentRating.stars;
      this.newFeedback = this.currentRating.feedback;
    }
    else{
      this.newRating = 0;
      this.newFeedback = '';
    }
  }

  postNewRating(){
    this.ratingService.addRating(this.currentCourseId, this.newRating, this.newFeedback).subscribe(
      (res) => {
        this.newRating = res.stars;
        this.newFeedback = res.feedback;
      }
    );
  }

  editRating(){
    this.ratingService.editRating(this.currentRating!.id, this.newRating, this.newFeedback).subscribe(
      (res) => {
        this.newRating = res.stars;
        this.newFeedback = res.feedback;
      }
    );
  }

  deleteRating(){
    this.ratingService.deleteRating(this.currentRating!.id).subscribe(
      (res) => {
        this.newRating = 0;
        this.newFeedback = '';
        this.currentRating = undefined;
      }
    );
  }
}
