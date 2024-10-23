import { Routes } from '@angular/router';
import { SignUpComponent } from './auth/components/sign-up/sign-up.component';
import { SignInComponent } from './auth/components/sign-in/sign-in.component';
import { AccountActivationComponent } from './auth/components/account-activation/account-activation.component';
import { AddCourseComponent } from './course/components/add-course/add-course.component';
import { CoursesListComponent } from './course/components/courses-list/courses-list.component';
import { EditProfileComponent } from './user/components/edit-profile/edit-profile.component';
import { HeroComponent } from './core/components/hero/hero.component';
import { CoursesComponent } from './course/components/courses/courses.component';

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
    path: 'user/edit-profile',
    component: EditProfileComponent,
  },
];
