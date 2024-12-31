import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '../../../auth/services/auth.service';
import { CoursesService } from '../../../course/services/course.service';
import { CategoryService } from '../../../course/services/category.service';
import { UserService } from '../../../user/services/user.service';
import { CourseCarouselComponent } from '../../../course/components/displays/courses-carousels/course-carousel/course-carousel.component';
import { DialogModule } from 'primeng/dialog';
import { ResetPasswordComponent } from '../../../auth/components/reset-password/reset-password.component';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [
    ButtonModule,
    RouterLink,
    CourseCarouselComponent,
    DialogModule,
    ResetPasswordComponent,
  ],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css',
})
export class HeroComponent implements OnInit {
  coursesService = inject(CoursesService);
  cetegoriesService = inject(CategoryService);

  authService = inject(AuthService);
  route = inject(ActivatedRoute);
  userService = inject(UserService);

  userId: string | null = null;
  activationToken: string | null = null;
  user = this.userService.currentUser;

  selectedCategories = computed(() =>
    this.cetegoriesService
      .categories().filter(c => c.name !== 'Other')
      .sort(() => 0.5 - Math.random())
      .slice(0, 3)
  );

  courseCaruseles = computed(() => {
    return this.selectedCategories().map((category) => {
      const courses = this.coursesService.getCouresByCategoryId(category.id);
      return { category, courses };
    });
  });

  resetPasswordVisible = false;
  resetPasswordToken = signal<string | null>(null);

  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParamMap.get('userId');
    this.activationToken =
      this.route.snapshot.queryParamMap.get('activationToken');

    this.resetPasswordToken.set(
      this.route.snapshot.queryParamMap.get('passwordResetToken')
    );

    if (this.resetPasswordToken() !== null && this.resetPasswordToken() !== '')
      this.resetPasswordVisible = true;

    this.activateAccount();
  }

  activateAccount() {
    if (this.userId !== null && this.activationToken !== null)
      this.authService
        .activateAccount(this.userId, this.activationToken)
        .subscribe();
  }
}
