import { Component, computed, ElementRef, inject, input, OnInit, Renderer2, signal, ViewChild } from '@angular/core';
import { CoursesCarouselsComponent } from '../courses-carousels/courses-carousels.component';
import { CoursesService } from '../../services/course.service';
import { CourseDetail } from '../../models/course.model';
import { FinanceService } from '../../../finance/finance.service';
import { User, UserDetail } from '../../../user/models/user.model';
import { UserService } from '../../../user/services/user.service';
import { CourseContentService } from '../../services/course-content-service';
import { AccordionModule } from 'primeng/accordion';
import { SectionItemComponent } from "../edit-course/course-creator/section-item/section-item.component";
import { ViewElementItemComponent } from "./view-element-item/view-element-item.component";

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [AccordionModule, SectionItemComponent, ViewElementItemComponent],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css'
})
export class CourseDetailComponent implements OnInit {
  changeSectionEditMode(event: boolean){

  }
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);
  author = signal<UserDetail | null>(null);
  sections = computed(() => this.courseContentService.sections());

  //Services
  courseService = inject(CoursesService);
  financeService = inject(FinanceService);
  userService = inject(UserService);
  courseContentService = inject(CourseContentService);

  items = this.financeService.items;



  courseItem = computed(() => {
    const item = this.items().find(item => item.id === this.courseId())
    return {
      ...this.course,
      price: {
        amount: item?.price.amount ?? 0,
      },
    };
  });

  ngOnInit(): void {
    this.courseService.getCourseById(this.courseId()).subscribe({
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
