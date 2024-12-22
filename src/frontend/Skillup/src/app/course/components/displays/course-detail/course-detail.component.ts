import { Component, computed, ElementRef, inject, input, OnChanges, OnInit, Renderer2, signal, SimpleChanges, ViewChild } from '@angular/core';

import { ViewElementItemComponent } from "./view-element-item/view-element-item.component";
import { Router } from '@angular/router';
import { CourseItemComponent } from "../course-item/course-item.component";
import { Rating, RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { CourseDetail } from '../../../models/course.model';
import { SectionItemComponent } from '../../edit-course/course-creator/section-item/section-item.component';
import { AccordionModule } from 'primeng/accordion';
import { UserDetail } from '../../../../user/models/user.model';
import { CoursesService } from '../../../services/course.service';
import { FinanceService } from '../../../../finance/services/finance.service';
import { UserService } from '../../../../user/services/user.service';
import { CourseContentService } from '../../../services/course-content.service';
import { CourseRatingService } from '../../../services/course-rating.service';
import { CourseDetailRating } from '../../../models/rating.model';
import { CarouselModule } from 'primeng/carousel';
import { BuyButtonComponent } from "../../buy-button/buy-button.component";
import { AuthorDescriptionComponent } from "../author-description/author-description.component";

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [AccordionModule, SectionItemComponent, ViewElementItemComponent, CourseItemComponent, RatingModule, FormsModule, CarouselModule, BuyButtonComponent, AuthorDescriptionComponent],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css'
})
export class CourseDetailComponent implements OnChanges {
  courseId = input.required<string>();
  
  //Services
  courseService = inject(CoursesService);
  financeService = inject(FinanceService);
  userService = inject(UserService);
  courseContentService = inject(CourseContentService);
  ratingService = inject(CourseRatingService);
  router = inject(Router);

  //Variables
  course = signal<CourseDetail | null>(null);
  author = signal<UserDetail | null>(null);
  sections = computed(() => this.courseContentService.sections());
  coursesForAuthor = computed(() =>  {
    return this.courseService.getCoursesByAuthor(this.author()!.id).slice(0, 10)
});
coursesForCategory = computed(() =>  {
  return this.courseService.getCouresByCategoryId(this.course()!.category.id).slice(0, 10)
});
courseRating: CourseDetailRating | undefined = undefined
  rating = 0;
  numberOfRating = 1567;
  totalCourseTime = 68;
  lastUpdate = '05.07.2024';
  courseListItem = computed(() => this.courseService.courses().find(c => c.id === this.courseId()));

  items = this.financeService.items;
  courseItem = computed(() => {
    const item = this.items().find(item => item.id === this.courseId())
    return {
      ...this.course,
      price:  item?.price ?? 0,
    };
  });

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['courseId']) {
      this.courseService.getCourseDetailById(this.courseId()).subscribe({
        next: (res) => {
          this.course.set(res);
          this.userService.getUser(this.course()!.authorId).subscribe({
            next: (res) => {
              this.author.set(res);
            }
          });
        }
      })
      
     this.courseContentService.getSectionsByCourseId(this.courseId());

     this.ratingService.getCourseDetailRating(this.courseId(), 10).subscribe((res) => {
      this.courseRating = res;
      this.rating = this.courseRating.rating.averageStars;
     })

    window.scrollTo({ top: 0, behavior: 'instant' });
    }
  }

  //Changing styles while scrolling
  @ViewChild('target') target!: ElementRef;
@ViewChild('startTrigger') startTrigger!: ElementRef;
@ViewChild('endTrigger') endTrigger!: ElementRef;
  constructor(private renderer: Renderer2) {}

  ngAfterViewInit(): void {
    const observerStart = new IntersectionObserver(entries => {
      entries.forEach(entry => {
        if (!entry.isIntersecting) {
          this.renderer.removeClass(this.target.nativeElement, 'not-visible');
        } else {
          this.renderer.addClass(this.target.nativeElement, 'not-visible');
        }
      });
    });
  
    const observerEnd = new IntersectionObserver(entries => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          this.renderer.addClass(this.target.nativeElement, 'not-visible');
        } else {
          this.renderer.removeClass(this.target.nativeElement, 'not-visible');
        }
      });
    });
  
    observerStart.observe(this.startTrigger.nativeElement);
    observerEnd.observe(this.endTrigger.nativeElement);
  }
}
