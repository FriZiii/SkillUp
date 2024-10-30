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

  selectedCategories = signal<Category[]>([]);

  courseCaruseles = computed(() => {
    console.log(this.selectedCategories());

    return this.selectedCategories().map((x) =>
      this.coursesService.getCouresByCategoryId(x.id)
    );
  });

  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParamMap.get('userId');
    this.activationToken =
      this.route.snapshot.queryParamMap.get('activationToken');

    const categories = this.cetegoriesService.categories();

    this.selectedCategories.set([
      ...new Set(
        Array.from(
          { length: 3 },
          () => categories[Math.floor(Math.random() * categories.length)]
        )
      ),
    ]);

    this.activateAccount();
  }

  activateAccount() {
    if (this.userId !== null && this.activationToken !== null)
      this.authService
        .activateAccount(this.userId, this.activationToken)
        .subscribe();
  }
}
