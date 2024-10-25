import { Component, Input, input } from '@angular/core';
import { CourseListItem } from '../../models/course.model';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-course-item',
  standalone: true,
  imports: [ButtonModule, CardModule],
  templateUrl: './course-item.component.html',
  styles: []
})
export class CourseItemComponent {
  @Input() course!: CourseListItem;
}
