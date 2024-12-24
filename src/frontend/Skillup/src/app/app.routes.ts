import { Routes } from '@angular/router';
import { SignUpComponent } from './auth/components/sign-up/sign-up.component';
import { SignInComponent } from './auth/components/sign-in/sign-in.component';
import { AddCourseComponent } from './course/components/add-course/add-course.component';
import { HeroComponent } from './core/components/hero/hero.component';
import { EditUserComponent } from './user/components/edit-user/edit-user.component';
import { EditUserProfileComponent } from './user/components/edit-user/edit-user-profile/edit-user-profile.component';
import { EditUserPictureComponent } from './user/components/edit-user/edit-user-picture/edit-user-picture.component';
import { EditUserPrivacySettingsComponent } from './user/components/edit-user/edit-user-privacy-settings/edit-user-privacy-settings.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { AccessDeniedComponent } from './core/components/access-denied/access-denied.component';
import { UserRole } from './user/models/user-role.model';
import { hasRole, isAuthor, isSignedIn } from './core/guards/auth.guard';
import { OtherUserProfileComponent } from './user/components/other-user-profile/other-user-profile.component';
import { EditCourseComponent } from './course/components/edit-course/edit-course.component';
import { CourseCreatorComponent } from './course/components/edit-course/course-creator/course-creator.component';
import { CoursePricingComponent } from './course/components/edit-course/course-pricing/course-pricing.component';
import { CourseEssentialsComponent } from './course/components/edit-course/course-essentials/course-essentials.component';
import { AddAssignmentComponent } from './course/components/edit-course/assignment/add-assignment/add-assignment.component';
import { AssignmentComponent } from './course/components/edit-course/assignment/assignment.component';
import { CanEnterAddAssignment } from './core/guards/canEnterAddAssignment.guard';
import { YourCoursesComponent } from './course/components/your-courses/your-courses.component';
import { CoursesCreatedByYouComponent } from './course/components/courses-created-by-you/courses-created-by-you.component';
import { CartComponent } from './finance/components/cart/cart.component';
import { OrderSummaryComponent } from './finance/components/order-summary/order-summary.component';
import { BalanceComponent } from './finance/components/balance/balance.component';
import { CoursesToReviewComponent } from './course/components/reviews/course-reviews/courses-to-review.component';
import { CoursesCarouselsComponent } from './course/components/displays/courses-carousels/courses-carousels.component';
import { CoursesListComponent } from './course/components/displays/courses-list/courses-list.component';
import { CourseDetailComponent } from './course/components/displays/course-detail/course-detail.component';
import { CourseReviewComponent } from './course/components/reviews/course-review/course-review.component';
import { CourseWalkThroughComponent } from './course/components/course-walk-through/course-walk-through.component';
import { SolveQuizComponent } from './course/components/exercises/solve-quiz/solve-quiz.component';
import { SolveQuestionComponent } from './course/components/exercises/solve-question/solve-question.component';
import { EditUserPasswordComponent } from './user/components/edit-user/edit-user-password/edit-user-password.component';
import { SolveFillTheGapComponent } from './course/components/exercises/solve-fill-the-gap/solve-fill-the-gap.component';
import { OrderPageComponent } from './finance/components/order-page/order-page.component';
import { ChatsComponent } from './chat/components/chats/chats.component';

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
    path: 'element-edit/:elementId/add-assignment',
    component: AddAssignmentComponent,
    canActivate: [CanEnterAddAssignment],
  },
  {
    path: 'element-edit/:elementId/assignment',
    component: AssignmentComponent,
  },
  {
    path: 'user/:userId',
    component: OtherUserProfileComponent,
  },
  {
    path: 'user/edit',
    component: EditUserComponent,
    canMatch: [isSignedIn],
    children: [
      { path: 'profile', component: EditUserProfileComponent },
      { path: 'profile-picture', component: EditUserPictureComponent },
      { path: 'privacy-settings', component: EditUserPrivacySettingsComponent },
      { path: 'change-password', component: EditUserPasswordComponent },
    ],
  },
  {
    path: 'user/:userId/courses',
    component: YourCoursesComponent,
    canMatch: [isSignedIn],
  },
  {
    path: 'author/:authorId/courses',
    component: CoursesCreatedByYouComponent,
    canMatch: [hasRole],
    data: { requiredRole: UserRole.Instructor },
  },
  {
    path: 'cart',
    component: CartComponent,
  },
  {
    path: 'order-summary',
    component: OrderSummaryComponent,
  },
  {
    path: 'balance',
    component: BalanceComponent,
  },
  {
    path: 'order/:orderId',
    component: OrderPageComponent,
  },
  {
    path: 'reviews',
    component: CoursesToReviewComponent,
    canMatch: [hasRole],
    data: { requiredRole: UserRole.Moderator },
  },
  {
    path: 'course/:courseId/review',
    component: CourseReviewComponent,
    canMatch: [hasRole],
    data: { requiredRole: UserRole.Moderator },
  },
  {
    path: 'course/:courseId/walk-through',
    component: CourseWalkThroughComponent,
  },
  {
    path: 'quiz',
    component: SolveQuizComponent,
  },
  {
    path: 'fillgap',
    component: SolveFillTheGapComponent,
  },
  {
    path: 'access-denied',
    component: AccessDeniedComponent,
  },
  {
    path: 'chats',
    component: ChatsComponent,
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];
