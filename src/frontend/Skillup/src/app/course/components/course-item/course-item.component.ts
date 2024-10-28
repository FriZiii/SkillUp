import { Component, Input, input, OnInit } from '@angular/core';
import { Course, CourseListItem } from '../../models/course.model';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { TruncatePipe } from "../../../utils/pipes/truncate.pipe";

@Component({
  selector: 'app-course-item',
  standalone: true,
  imports: [ButtonModule, CardModule, TruncatePipe, TooltipModule],
  templateUrl: './course-item.component.html',
  styleUrl: './course-item.component.css',
})
export class CourseItemComponent{
  @Input() course!: CourseListItem;
}
