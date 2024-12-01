import { Component, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CourseListItem } from '../../../course/models/course.model';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [InputTextModule, FormsModule, ButtonModule],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent {
  courses = input.required<CourseListItem[]>();
  onFilter =  output<CourseListItem[]>();
  title = signal('');

  filter(){
    const filteredCourses = this.courses().filter(item =>  item.title.toLowerCase().includes(this.title().toLowerCase()));
    this.onFilter.emit(filteredCourses);
  }
}
