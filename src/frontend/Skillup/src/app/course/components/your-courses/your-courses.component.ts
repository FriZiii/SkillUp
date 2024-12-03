import { Component, input } from '@angular/core';

@Component({
  selector: 'app-your-courses',
  standalone: true,
  imports: [],
  templateUrl: './your-courses.component.html',
  styleUrl: './your-courses.component.css'
})
export class YourCoursesComponent {
  userId = input<string>();
}
