import { Routes } from '@angular/router';
import { SignUpComponent } from './auth/components/sign-up/sign-up.component';
import { SignInComponent } from './auth/components/sign-in/sign-in.component';
import { AccountActivationComponent } from './auth/components/account-activation/account-activation.component';
import { AddCourseComponent } from './course/components/add-course/add-course.component';
import { CoursesListComponent } from './course/components/courses-list/courses-list.component';
import { HeroComponent } from './core/components/hero/hero.component';
import { CoursesComponent } from './course/components/courses/courses.component';
import { EditUserComponent } from './user/components/edit-user/edit-user.component';
import { EditUserProfileComponent } from './user/components/edit-user/edit-user-profile/edit-user-profile.component';
import { EditUserPictureComponent } from './user/components/edit-user/edit-user-picture/edit-user-picture.component';
import { EditUserPrivacySettingsComponent } from './user/components/edit-user/edit-user-privacy-settings/edit-user-privacy-settings.component';

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
    path: 'course/list',
    component: CoursesListComponent,
  },
  {
    path: 'courses/:category/:subcategory',
    component: CoursesComponent,
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
