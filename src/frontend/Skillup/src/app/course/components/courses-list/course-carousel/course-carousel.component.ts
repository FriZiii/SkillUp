import { Component, input, OnInit } from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { CourseListItem } from '../../../models/course.model';
import { CarouselModule } from 'primeng/carousel';

@Component({
  selector: 'app-course-carousel',
  standalone: true,
  imports: [CourseItemComponent, CarouselModule],
  templateUrl: './course-carousel.component.html',
  styleUrl: './course-carousel.component.css',
})
export class CourseCarouselComponent implements OnInit {
  
  courses = input.required<CourseListItem[]>();
  responsiveOptions: any[] | undefined;

  ngOnInit(): void {
    this.responsiveOptions = [
      {
          breakpoint: '1100px',
          numVisible: 2,
          numScroll: 1
      },
      {
          breakpoint: '767px',
          numVisible: 1,
          numScroll: 1
      }
  ];
  }
}