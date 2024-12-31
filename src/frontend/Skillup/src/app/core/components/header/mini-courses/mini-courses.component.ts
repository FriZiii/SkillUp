import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { PurchasedItemsService } from '../../../../course/services/purchasedItems.service';
import { UserService } from '../../../../user/services/user.service';
import { UserProgressService } from '../../../../course/services/user-progress-service';
import { CoursePercentage } from '../../../../course/models/user-progress.model';
import { RouterModule } from '@angular/router';
import { TruncatePipe } from "../../../../utils/pipes/truncate.pipe";
import { CircleProgressComponent } from "../../../../utils/components/circle-progress/circle-progress.component";

@Component({
  selector: 'app-mini-courses',
  standalone: true,
  imports: [RouterModule, TruncatePipe, CircleProgressComponent],
  templateUrl: './mini-courses.component.html',
  styleUrl: './mini-courses.component.css'
})
export class MiniCoursesComponent implements OnInit {
   //Services
   purchasedItemsService = inject(PurchasedItemsService);
   userService = inject(UserService);
   userProgressService = inject(UserProgressService);

  //Variables
  user = this.userService.currentUser;
  coursePercentages = computed(() => this.userProgressService.percentages());
  courses = this.purchasedItemsService.purchasedCourses;
  coursesWithPercentage = computed(() => this.courses().map(course => {
    const percentage = this.coursePercentages().find(p => p.courseId === course.id);
    return { ...course, percentage: percentage ? percentage.percentage : 0 };
  }))

   ngOnInit(): void {
    this.userProgressService.getPercentage().subscribe((res) => {
    })
  }
}
