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
import { UserProgressService } from '../../services/user-progress-service';
import { CoursePercentage } from '../../models/user-progress.model';
import { StudentCourseItemComponent } from "../displays/student-course-item/student-course-item.component";
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-your-courses',
  standalone: true,
  imports: [ProgressSpinnerModule, DialogModule, ButtonModule, RatingModule, FormsModule, InputTextModule, StudentCourseItemComponent, InputTextareaModule, FloatLabelModule],
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
   userProgressService = inject(UserProgressService);
  
  //Variables
  loading = true;
  courses = this.courseService.purchasedCourses;
  courseRatings: UserRating[] = []
  currentRating: UserRating | undefined = undefined;
  addRatingDialogVisible = false;
  newRating = 0;
  newFeedback = '';
  currentCourseId = '';
  coursePercentages = signal<CoursePercentage[]>([]);

  ngOnInit(): void {
    this.loading = false;

    this.ratingService.getUserRatings(this.userService.currentUser()!.id).subscribe(
      (res) => this.courseRatings = res
    );

    this.userProgressService.getPercentage().subscribe((res) => {
      this.coursePercentages.set(res);
      console.log(res)
    })
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
        this.addRatingDialogVisible = false;
      }
    );
  }

  editRating(){
    this.ratingService.editRating(this.currentRating!.id, this.newRating, this.newFeedback).subscribe(
      (res) => {
        this.newRating = res.stars;
        this.newFeedback = res.feedback;
        this.addRatingDialogVisible = false;
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

  getPercentage(courseId: string){
    console.log( this.coursePercentages())
    return this.coursePercentages().find(p => p.courseId === courseId);
  }
}
