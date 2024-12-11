import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { CourseDetail } from '../../../models/course.model';
import { CoursesService } from '../../../services/course.service';
import { CourseReviewService } from '../../../services/course-review.service';
import { SectionItemComponent } from "../../edit-course/course-creator/section-item/section-item.component";
import { AccordionModule } from 'primeng/accordion';
import { CourseContentService } from '../../../services/course-content.service';
import { ElementListComponent } from "../../edit-course/course-creator/element-list/element-list.component";
import { ElementItemDisplayComponent } from "../../element-item-display/element-item-display.component";
import { Review } from '../../../models/review.model';

@Component({
  selector: 'app-course-review',
  standalone: true,
  imports: [SectionItemComponent, AccordionModule, ElementItemDisplayComponent],
  templateUrl: './course-review.component.html',
  styleUrl: './course-review.component.css'
})
export class CourseReviewComponent implements OnInit {
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);
  sections = computed(() => this.courseContentService.sections());
  review = signal<Review | null>(null);

  //Services
  courseService = inject(CoursesService);
  courseContentService = inject(CourseContentService);
  reviewService = inject(CourseReviewService);

  ngOnInit(): void {
    this.courseContentService.getSectionsByCourseId(this.courseId());
    this.courseService.getCourseDetailById(this.courseId()).subscribe(
      (res) => {
        this.course.set(res);
      }
    )
    this.reviewService.getLatestReviewByCourse(this.courseId()).subscribe(
      (res) => this.review.set(res)
    )
  }

  addComment(elementId: string){
    console.log(elementId);
    this.reviewService.addComment(this.review()!.id, elementId, 'Komentarz').subscribe();
  }
}
