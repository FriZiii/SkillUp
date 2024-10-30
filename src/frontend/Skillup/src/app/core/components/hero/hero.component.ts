import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CourseCarouselComponent } from '../../../course/components/courses-carousels/course-carousel/course-carousel.component';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [ButtonModule, RouterLink, CourseCarouselComponent],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css',
})
export class HeroComponent implements OnInit {
  ngOnInit(): void {}
}
