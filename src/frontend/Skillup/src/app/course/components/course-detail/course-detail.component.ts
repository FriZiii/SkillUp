import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { CoursesCarouselsComponent } from '../courses-carousels/courses-carousels.component';
import { CoursesService } from '../../services/course.service';
import { CourseDetail } from '../../models/course.model';
import { FinanceService } from '../../../finance/finance.service';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css'
})
export class CourseDetailComponent implements OnInit {
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);

  //Services
  courseService = inject(CoursesService);
  financeService = inject(FinanceService);

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
      }
    })
  }
}
