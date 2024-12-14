import { Component, computed, inject, input, OnInit } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { UserProgressService } from '../../services/user-progress-service';
import { UserService } from '../../../user/services/user.service';
import { CourseContentService } from '../../services/course-content.service';
import { AccordionModule } from 'primeng/accordion';
import { SectionItemComponent } from "../edit-course/course-creator/section-item/section-item.component";
import { ElementItemDisplayComponent } from '../displays/element-item-display/element-item-display.component';

@Component({
  selector: 'app-course-walk-through',
  standalone: true,
  imports: [AccordionModule, SectionItemComponent, ElementItemDisplayComponent],
  templateUrl: './course-walk-through.component.html',
  styleUrl: './course-walk-through.component.css'
})
export class CourseWalkThroughComponent implements OnInit{
  //URL
  courseId = input.required<string>();

  //Services
  coureContentService = inject(CourseContentService);
  userProgressService = inject(UserProgressService);

  //Variables
  sections = computed(() => this.coureContentService.sections());

  ngOnInit(): void {
    this.coureContentService.getSectionsByCourseId(this.courseId());

  }
}
