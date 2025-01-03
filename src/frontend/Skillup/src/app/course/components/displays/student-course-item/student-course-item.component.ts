import { Component, computed, inject, input, OnChanges, output, SimpleChanges } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Course } from '../../../models/course.model';
import { CoursePercentage } from '../../../models/user-progress.model';
import { RatingModule } from 'primeng/rating';
import { CoursesService } from '../../../services/course.service';
import { FormsModule } from '@angular/forms';
import { CircleProgressComponent } from "../../../../utils/components/circle-progress/circle-progress.component";
import { RouterModule } from '@angular/router';
import { UserRating } from '../../../models/rating.model';
import { CertificateService } from '../../../services/certificate.service';

@Component({
  selector: 'app-student-course-item',
  standalone: true,
  imports: [RatingModule, FormsModule, CircleProgressComponent, RouterModule, ButtonModule],
  templateUrl: './student-course-item.component.html',
  styleUrl: './student-course-item.component.css'
})
export class StudentCourseItemComponent  {
  course = input.required<Course>();
  onRating = output<string>();
  percentages = input<CoursePercentage[]>([])
  userRatings = input.required<UserRating[]>();

  //Services
  courseService = inject(CoursesService);
  certificateService = inject(CertificateService);
  

  //Variables
  percentage = computed(() => this.percentages().find(x => x.courseId===this.course().id))
  userRating = computed(() => 
    this.userRatings().find(r => r.courseId === this.course().id) ?? {
      id: '1',
    courseId: '1',
    ratedById: '1',
    stars: 0,
    feedback: '',
    time: new Date(),
    }
)

  addRating(courseId: string){
    this.onRating.emit(courseId);
  }

  download(){
    this.certificateService.getCertificate(this.course().id).subscribe((blob) => {
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(blob);
      a.href = objectUrl;
      a.download = this.course().title + "-certificate";
      a.click();
      URL.revokeObjectURL(objectUrl);
    });
  }
}
