import { Component, computed, ElementRef, inject, input, OnChanges, OnInit, Renderer2, signal, SimpleChanges, ViewChild } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { CourseDetail } from '../../models/course.model';
import { FinanceService } from '../../../finance/services/finance.service';
import { User, UserDetail } from '../../../user/models/user.model';
import { UserService } from '../../../user/services/user.service';
import { CourseContentService } from '../../services/course-content-service';
import { AccordionModule } from 'primeng/accordion';
import { SectionItemComponent } from "../edit-course/course-creator/section-item/section-item.component";
import { ViewElementItemComponent } from "./view-element-item/view-element-item.component";
import { Router } from '@angular/router';
import { CourseItemComponent } from "../course-item/course-item.component";
import { Rating, RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [AccordionModule, SectionItemComponent, ViewElementItemComponent, CourseItemComponent, RatingModule, FormsModule],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css'
})
export class CourseDetailComponent implements OnChanges {
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);
  author = signal<UserDetail | null>(null);
  sections = computed(() => this.courseContentService.sections());
  coursesForAuthor = computed(() =>  {
    return this.courseService.getCoursesByAuthor(this.author()!.id).slice(0, 10)
});
coursesForCategory = computed(() =>  {
  return this.courseService.getCouresByCategoryId(this.course()!.category.id).slice(0, 10)
});
  rating = 3;
  numberOfRating = 1567;
  numberOfParticipians = 1945;
  totalCourseTime = 68;
  lastUpdate = '05.07.2024';


  //Services
  courseService = inject(CoursesService);
  financeService = inject(FinanceService);
  userService = inject(UserService);
  courseContentService = inject(CourseContentService);
  router = inject(Router);

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

    window.scrollTo({ top: 0, behavior: 'instant' });
    }
  }

  navigateToAuthor() {
    this.router.navigate(['/user', this.author()?.id]);
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
