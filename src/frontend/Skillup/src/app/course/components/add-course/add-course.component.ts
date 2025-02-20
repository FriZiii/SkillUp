import {
  Component,
  computed,
  DestroyRef,
  inject,
  signal,
} from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { StepperModule } from 'primeng/stepper';
import { ButtonModule } from 'primeng/button';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { CardModule } from 'primeng/card';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { CoursesService } from '../../services/course.service';
import { BehaviorSubject, finalize, Observable } from 'rxjs';
import { ToastHandlerService } from '../../../core/services/toast-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-course',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    StepperModule,
    ButtonModule,
    ToggleButtonModule,
    CardModule,
    InputTextModule,
    SelectModule,
  ],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css',
})
export class AddCourseComponent {
  //Services
  courseCategoryService = inject(CategoryService);
  courseService = inject(CoursesService);
  toastService = inject(ToastHandlerService);

  //Variables
  loading = false;
  courseId = null;
  router = inject(Router);
  categories = this.courseCategoryService.categories;
  categoryNames = computed(() =>
    this.categories().map((category) => ({
      id: category.id,
      name: category.name,
    }))
  );
  selectedCategory = signal('');
  subcategoryNames = computed(() => {
    const selectedCat = this.categories().find(
      (category) => category.id === this.selectedCategory()
    );
    return selectedCat
      ? selectedCat.subcategories.map((sub) => ({ id: sub.id, name: sub.name }))
      : [];
  });

  destroyRef = inject(DestroyRef);

  //Form
  form = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.maxLength(32)]),
    category: new FormControl('', [Validators.required]),
    subcategory: new FormControl('', [Validators.required]),
  });

  onSubmit() {
    this.loading = true;
    const title = this.form.value.title;
    const category = this.form.value.category;
    const subcategory = this.form.value.subcategory;
    const subscription = this.courseService
      .addCourse({
        title: title!,
        categoryId: category!,
        subcategoryId: subcategory!,
      })
      .pipe(finalize (() => this.loading = false))
      .subscribe({
        next: (res) => {
          this.courseId = res.id;
          this.toastService.showSuccess('Course sucessfully added');
          this.router.navigate(['/course-edit', this.courseId, 'essentials']);
        },
      });
    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe;
    });
  }

  get titleIsInvalid() {
    return this.form.controls.title.dirty && this.form.controls.title.invalid;
  }
}
