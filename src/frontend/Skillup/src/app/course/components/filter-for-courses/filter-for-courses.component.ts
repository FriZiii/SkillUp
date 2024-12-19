import { Component, input, OnChanges, output, SimpleChanges } from '@angular/core';
import { CourseListItem } from '../../models/course.model';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-for-courses',
  standalone: true,
  imports: [InputTextModule, FormsModule],
  templateUrl: './filter-for-courses.component.html',
  styleUrl: './filter-for-courses.component.css'
})
export class FilterForCoursesComponent implements OnChanges {
  courses = input.required<CourseListItem[]>();
  filteredCourses = output<CourseListItem[]>();

  title = '';
  authorName = '';

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['courses']){
      this.title = '';
      this.authorName = '';
    }
  }

  applyFilters() {
    const filtered = this.courses()
      .filter(course => {
        const matchesSearch = course.title?.toLowerCase().includes(this.title.toLowerCase());
        const matchesAuthor = course.authorName?.toLowerCase().includes(this.authorName.toLowerCase());
        return matchesSearch && matchesAuthor;
      });

    this.filteredCourses.emit(filtered);
  }
}
