import { Component } from '@angular/core';
import { AccordionModule } from 'primeng/accordion';

@Component({
  selector: 'app-course-creator',
  standalone: true,
  imports: [AccordionModule],
  templateUrl: './course-creator.component.html',
  styleUrl: './course-creator.component.css'
})
export class CourseCreatorComponent {

}
