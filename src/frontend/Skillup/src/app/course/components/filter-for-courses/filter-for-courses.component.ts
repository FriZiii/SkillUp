import { Component, computed, inject, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { CourseListItem } from '../../models/course.model';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
import { CourseLevel } from '../../models/course-level.model';
import { Slider, SliderModule } from 'primeng/slider';
import { InputNumberModule } from 'primeng/inputnumber';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-filter-for-courses',
  standalone: true,
  imports: [InputTextModule, FormsModule, SelectModule, SliderModule, InputNumberModule, FloatLabelModule],
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
  selectedStars = signal(0);
  selectedUsersCount = signal(0);
  selectedRatingsCount = signal(0);
  selectedPrice: number[] = [0, 1000];

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
  starsOptions = [
    {name: '5 stars', value: 5},
    {name: '4 stars and more ', value: 4},
    {name: '3 stars and more', value: 3},
    {name: '2 stars and more', value: 2},
    {name: 'All', value: -0.5},
  ]
  userCountOptions = [
    {name: '4 users and more', value: 4},
    {name: '3 users and more ', value: 3},
    {name: '2 users and more', value: 2},
    {name: '1 user and more', value: 1},
    {name: 'All', value: -0.5},
  ]
  ratingsCountOptions = [
    {name: '4 ratings and more', value: 4},
    {name: '3 ratings and more ', value: 3},
    {name: '2 ratings and more', value: 2},
    {name: '1 rating and more', value: 1},
    {name: 'All', value: -0.5},
  ]


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
        const matchesStars = course.averageRating >= this.selectedStars();
        const matchesUsers = course.usersCount >= this.selectedUsersCount();
        const matchesRatingCount = course.ratingsCount >= this.selectedRatingsCount();
        const matchesPrice = course.price >= this.selectedPrice[0] && course.price <= this.selectedPrice[1];
        return matchesSearch && matchesAuthor && matchesCategory && matchesSubcategory && matchesLevel && matchesStars && matchesUsers && matchesRatingCount && matchesPrice;
      });

    this.filteredCourses.emit(filtered);
  }
}
