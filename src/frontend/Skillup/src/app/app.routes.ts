import { Routes } from '@angular/router';
import { SignUpComponent } from './auth/components/sign-up/sign-up.component';
import { SignInComponent } from './auth/components/sign-in/sign-in.component';
import { AddCourseComponent } from './course/components/add-course/add-course.component';
import { HeroComponent } from './core/components/hero/hero.component';
import { CoursesListComponent } from './course/components/courses-list/courses-list.component';
import { EditUserComponent } from './user/components/edit-user/edit-user.component';
import { EditUserProfileComponent } from './user/components/edit-user/edit-user-profile/edit-user-profile.component';
import { EditUserPictureComponent } from './user/components/edit-user/edit-user-picture/edit-user-picture.component';
import { EditUserPrivacySettingsComponent } from './user/components/edit-user/edit-user-privacy-settings/edit-user-privacy-settings.component';
import { CoursesCarouselsComponent } from './course/components/courses-carousels/courses-carousels.component';
import { CourseDetailComponent } from './course/components/course-detail/course-detail.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { AccessDeniedComponent } from './core/components/access-denied/access-denied.component';
import { UserRole } from './user/models/user-role.model';
import { hasRole, isAuthor } from './core/guards/auth.guard';
import { OtherUserProfileComponent } from './user/components/other-user-profile/other-user-profile.component';
import { EditCourseComponent } from './course/components/edit-course/edit-course.component';
import { CourseCreatorComponent } from './course/components/edit-course/course-creator/course-creator.component';
import { CoursePricingComponent } from './course/components/edit-course/course-pricing/course-pricing.component';
import { CourseEssentialsComponent } from './course/components/edit-course/course-essentials/course-essentials.component';

export const routes: Routes = [
  {
    path: '',
    component: HeroComponent,
  },
  {
    path: 'sign-in',
    component: SignInComponent,
  },
  {
    path: 'sign-up',
    component: SignUpComponent,
  },
  {
    path: 'course/new',
    component: AddCourseComponent,
    canMatch: [hasRole],
    data: { requiredRole: UserRole.Instructor },
  },
  {
    path: 'course-carousels',
    component: CoursesCarouselsComponent,
  },
  {
    path: 'courses-list/:category/:subcategory',
    component: CoursesListComponent,
  },
  {
    path: 'course-detail/:courseId',
    component: CourseDetailComponent,
  },
  {
    path: 'course-edit/:courseId',
    component: EditCourseComponent,
    canMatch: [isAuthor],
    children: [
      { path: 'creator', component: CourseCreatorComponent },
      { path: 'essentials', component: CourseEssentialsComponent },
      { path: 'price', component: CoursePricingComponent },
      { path: 'landing-page', component: CourseDetailComponent },
    ],
  },
  {
    path: 'user/:userId',
    component: OtherUserProfileComponent,
  },
  {
    path: 'user/edit',
    component: EditUserComponent,
    children: [
      { path: 'profile', component: EditUserProfileComponent },
      { path: 'profile-picture', component: EditUserPictureComponent },
      { path: 'privacy-settings', component: EditUserPrivacySettingsComponent },
    ],
  },
  {
    path: 'access-denied',
    component: AccessDeniedComponent,
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];
