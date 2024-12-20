import { Component, computed, inject, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { CourseListItem } from '../../models/course.model';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
import { CourseLevel } from '../../models/course-level.model';

@Component({
  selector: 'app-filter-for-courses',
  standalone: true,
  imports: [InputTextModule, FormsModule, SelectModule],
  templateUrl: './filter-for-courses.component.html',
  styleUrl: './filter-for-courses.component.css'
})
export class FilterForCoursesComponent implements OnChanges {
  courses = input.required<CourseListItem[]>();
  filteredCourses = output<CourseListItem[]>();
  defaultCategory = input<string>('');
  defaultSubcategory = input<string>('');

  //Services
  courseCategoryService = inject(CategoryService)

  //Variables
  title = '';
  authorName = '';
  selectedCategory = signal('');
  selectedSubcategory = signal('');
  selectedLevel = signal<CourseLevel | null>(null);

  //Selects
  categories = this.courseCategoryService.categories;
  categoryNames = computed(() =>
    this.categories().map((category) => ({
      id: category.id,
      name: category.name,
    }))
  );
  subcategoryNames = computed(() => {
    var selectedCat : Category | undefined;
    if(this.defaultCategory() !== ''){
      const cat = this.categories().find(c => c.slug === this.defaultCategory());
      selectedCat = this.categories().find(
        (category) => category.id === cat?.id
      );
    }
    else{
      selectedCat = this.categories().find(
        (category) => category.id === this.selectedCategory()
      );
    }
    return selectedCat
      ? selectedCat.subcategories.map((sub) => ({ id: sub.id, name: sub.name }))
      : [];
  });
  levels = Object.entries(CourseLevel).map(([name, value]) => ({
    name,
    value
  }));


  ngOnChanges(changes: SimpleChanges): void {
    if(changes['courses']){
      this.title = '';
      this.authorName = '';
      this.selectedCategory.set('')
      this.selectedSubcategory.set('')
    }
  }

  applyFilters() {
    const filtered = this.courses()
      .filter(course => {
        const matchesSearch = course.title?.toLowerCase().includes(this.title.toLowerCase());
        const matchesAuthor = course.authorName?.toLowerCase().includes(this.authorName.toLowerCase());
        const matchesCategory = course.category?.id.includes(this.selectedCategory());
        const matchesSubcategory = course.category?.subcategory.id.includes(this.selectedSubcategory());
        const matchesLevel = this.selectedLevel() === CourseLevel.None || this.selectedLevel() === null ? course : course.level.includes(this.selectedLevel()!);
        return matchesSearch && matchesAuthor && matchesCategory && matchesSubcategory && matchesLevel;
      });

    this.filteredCourses.emit(filtered);
  }
}
