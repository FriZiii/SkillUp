import { Component, input, OnInit } from '@angular/core';
import { CourseItemComponent } from "../../course-item/course-item.component";
import { CarouselModule } from 'primeng/carousel';
import { SkeletonModule } from 'primeng/skeleton';
import { CourseListItem } from '../../../../models/course.model';

@Component({
  selector: 'app-course-carousel',
  standalone: true,
  imports: [CourseItemComponent, CarouselModule, SkeletonModule],
  templateUrl: './course-carousel.component.html',
  styleUrl: './course-carousel.component.css',
})
export class CourseCarouselComponent implements OnInit {
  
  courses = input<CourseListItem[]>([]);
  skeletonItems = new Array(5).fill('');
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
