import { Component, inject, input, OnInit, signal } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { Course } from '../../models/course.model';
import { CourseItemShortComponent } from "../course-item-short/course-item-short.component";
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AddNewDiscountCodeComponent } from "../../../finance/components/add-new-discount-code/add-new-discount-code.component";
import { DiscountCodeService } from '../../../finance/services/discountCode.service';
import { DiscountCode } from '../../../finance/models/discountCodes.model';

@Component({
  selector: 'app-courses-created-by-you',
  standalone: true,
  imports: [CourseItemShortComponent, ProgressSpinnerModule, AddNewDiscountCodeComponent],
  templateUrl: './courses-created-by-you.component.html',
  styleUrl: './courses-created-by-you.component.css'
})
export class CoursesCreatedByYouComponent implements OnInit {
  //Variables
  authorId = input<string>();
  courses = signal<Course[]>([]);
  loading = true;
  discountCodes = signal<DiscountCode[]>([]);

  //Services
  courseService = inject(CoursesService);
  discountCodeService = inject(DiscountCodeService);

  
  ngOnInit(): void {
    this.courseService.getCourseByAuthorId(this.authorId()!).subscribe(
      (res) => {
        this.courses.set(res);
        this.courses.set(this.courses().slice(0, 2));
        this.loading = false;
      }
    )
    this.discountCodeService.getDiscountCodesByOwner(this.authorId()!).subscribe(
      (res) => {
        this.discountCodes.set(res);
      }
    )
  }
}
