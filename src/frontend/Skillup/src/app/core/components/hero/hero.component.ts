import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CourseCarouselComponent } from '../../../course/components/courses-carousels/course-carousel/course-carousel.component';
import { AuthService } from '../../../auth/services/auth.service';
import { CoursesService } from '../../../course/services/course.service';
import { CategoryService } from '../../../course/services/category.service';
import { CourseListItem } from '../../../course/models/course.model';
import { Category } from '../../../course/models/category.model';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [ButtonModule, RouterLink, CourseCarouselComponent],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css',
})
export class HeroComponent implements OnInit {
  coursesService = inject(CoursesService);
  cetegoriesService = inject(CategoryService);

  authService = inject(AuthService);
  route = inject(ActivatedRoute);

  userId: string | null = null;
  activationToken: string | null = null;

  categories = this.cetegoriesService.categories;


  selectedCategories = computed(() => this.categories().sort(() => 0.5 - Math.random()).slice(0, 3));

  courseCaruseles = computed(() => {
    return this.selectedCategories().map(category => {
      const courses = this.coursesService.getCouresByCategoryId(category.id);
      return { category, courses };
    });
  });


  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParamMap.get('userId');
    this.activationToken =
      this.route.snapshot.queryParamMap.get('activationToken');

    this.activateAccount();
  }

  activateAccount() {
    if (this.userId !== null && this.activationToken !== null)
      this.authService
        .activateAccount(this.userId, this.activationToken)
        .subscribe();
  }
}
