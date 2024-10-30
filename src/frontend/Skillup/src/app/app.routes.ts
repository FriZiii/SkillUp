import { Routes } from '@angular/router';
import { SignUpComponent } from './auth/components/sign-up/sign-up.component';
import { SignInComponent } from './auth/components/sign-in/sign-in.component';
import { AccountActivationComponent } from './auth/components/account-activation/account-activation.component';
import { AddCourseComponent } from './course/components/add-course/add-course.component';
import { HeroComponent } from './core/components/hero/hero.component';
import { CoursesListComponent } from './course/components/courses-list/courses-list.component';
import { EditUserComponent } from './user/components/edit-user/edit-user.component';
import { EditUserProfileComponent } from './user/components/edit-user/edit-user-profile/edit-user-profile.component';
import { EditUserPictureComponent } from './user/components/edit-user/edit-user-picture/edit-user-picture.component';
import { EditUserPrivacySettingsComponent } from './user/components/edit-user/edit-user-privacy-settings/edit-user-privacy-settings.component';
import { CoursesCarouselsComponent } from './course/components/courses-carousels/courses-carousels.component';
import { CourseDetailComponent } from './course/components/course-detail/course-detail.component';

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
    path: 'account-activation',
    component: AccountActivationComponent,
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
];
