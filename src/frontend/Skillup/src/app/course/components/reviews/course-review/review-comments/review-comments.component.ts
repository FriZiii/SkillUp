import { AfterViewInit, Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { Review, ReviewComment } from '../../../../models/review.model';
import { CourseReviewService } from '../../../../services/course-review.service';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-review-comments',
  standalone: true,
  imports: [InputTextModule, ButtonModule, FormsModule, CommonModule],
  templateUrl: './review-comments.component.html',
  styleUrl: './review-comments.component.css'
})
export class ReviewCommentsComponent {
  elementId = input<string | null>(null);
  courseId = input<string>();
  moderator = input<boolean>(false);
  
  reviewService = inject(CourseReviewService);
  
  //Variables
  latestReview = computed(() => this.reviewService.latestReviewForCourse());
  allReviews = computed(() => this.reviewService.allReviewsForCourse()?.filter(r => r.id != this.latestReview()?.id));
  latestComment = computed(() => this.latestReview()?.comments.find(comment =>{
    if(this.elementId() !== null){
      return comment.courseElementId === this.elementId()}
    else{
      return comment.courseElementId === null;
    }}) || null)
  comments = computed(() => this.allReviews()?.flatMap(review => review.comments).filter(comment => {
    if(this.elementId() !== null){return comment.courseElementId === this.elementId()}
    else{
      return comment.courseElementId === null;}}) || null)
  newComment = '';

  addComment(){
    console.log(this.latestComment());
    this.reviewService.addComment(this.latestReview()!.id, this.courseId()!, this.elementId() ?? null, this.newComment).subscribe(
      (res) => {
      this.reviewService.latestReviewForCourse.set(res);
      this.newComment = '';
    });
  }

  deleteComment(commentId: string){
    this.reviewService.deleteComment(commentId).subscribe(
      (res) => {
        this.reviewService.getLatestReviewByCourse(this.courseId()!).subscribe((res) => {
          this.reviewService.latestReviewForCourse.set(res);
        })
      });
  }
}
