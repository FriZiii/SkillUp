import { Component, computed, DestroyRef, effect, inject, OnInit, signal, Signal } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
import { StepperModule } from 'primeng/stepper';
import { ButtonModule } from 'primeng/button';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { CardModule } from 'primeng/card';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { CoursesService } from '../../services/course.service';

@Component({
  selector: 'app-add-course',
  standalone: true,
  imports: [ReactiveFormsModule, StepperModule, ButtonModule, ToggleButtonModule, CardModule, InputTextModule, SelectModule],
  providers: [CategoryService],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css',
})
export class AddCourseComponent implements OnInit {
  //Services
  courseCategoryService = inject(CategoryService);
  courseService = inject(CoursesService);

  //Variables
  categories= signal<Category[]>([]);
  categoryNames = computed(() => this.categories().map(category => ({id: category.id, name: category.name})));
  selectedCategory = '';
  subcategoryNames = computed(() => {
    const selectedCat = this.categories().find(category => category.id === this.selectedCategory);
    return selectedCat ? selectedCat.subcategories.map(sub => ({id: sub.id, name: sub.name})) : [];
  });

  destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    this.courseCategoryService.getCategories().subscribe((data) => {
      this.categories.set(data);
    });
  }

  //Form
  form: FormGroup = new FormGroup({
    title: new FormControl('', {validators: [Validators.required],}),
    category: new FormControl('', {validators: [Validators.required],}),
    subcategory: new FormControl('', {validators: [Validators.required],}),
  });

  onSubmit(){
    const title = this.form.value.title;
    const category = this.form.value.category;
    const subcategory = this.form.value.subcategory;
    console.log(title);
    console.log(category);
    console.log(subcategory);
    const subscription = this.courseService.addCourse({title: title, categoryId: category, subcategoryId: subcategory}).subscribe({
      next: (res) => {
        console.log(res)
      }
    })
    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }
}