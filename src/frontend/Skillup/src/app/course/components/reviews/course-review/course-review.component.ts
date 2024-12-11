import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { CourseDetail } from '../../../models/course.model';
import { CoursesService } from '../../../services/course.service';
import { CourseReviewService } from '../../../services/course-review.service';
import { SectionItemComponent } from "../../edit-course/course-creator/section-item/section-item.component";
import { AccordionModule } from 'primeng/accordion';
import { CourseContentService } from '../../../services/course-content.service';
import { ElementItemDisplayComponent } from "../../element-item-display/element-item-display.component";
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-course-review',
  standalone: true,
  imports: [SectionItemComponent, AccordionModule, ElementItemDisplayComponent, DialogModule, ButtonModule],
  templateUrl: './course-review.component.html',
  styleUrl: './course-review.component.css'
})
export class CourseReviewComponent implements OnInit {
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);
  sections = computed(() => this.courseContentService.sections());
  commentDialogVisible=false;
  currentElementId = '';
  
  //Services
  courseService = inject(CoursesService);
  courseContentService = inject(CourseContentService);
  reviewService = inject(CourseReviewService);

  courseListItem = computed(() => this.courseService.courses().find(c => c.id === this.courseId()) || null);
  
  ngOnInit(): void {
    this.courseContentService.getSectionsByCourseId(this.courseId());
    this.courseService.getCourseDetailById(this.courseId()).subscribe(
      (res) => {
        this.course.set(res);
      }
    )
    this.reviewService.getReviewsByCourse(this.courseId()).subscribe(
      (res) => {
        this.reviewService.allReviewsForCourse.set(res);
      }
    );
    this.reviewService.getLatestReviewByCourse(this.courseId()).subscribe(
      (res) => {
        this.reviewService.latestReviewForCourse.set(res);
      });
  }
}
