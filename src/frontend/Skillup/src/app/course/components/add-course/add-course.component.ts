import { Component, DestroyRef, inject, signal } from '@angular/core';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-add-course',
  standalone: true,
  imports: [],
  providers: [CategoryService],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css'
})
export class AddCourseComponent {
  categoryService = inject(CategoryService);
  categories = this.categoryService.categories();
  private destroyRef = inject(DestroyRef);
  error = signal('');

  ngOnInit(){
    const subscription = this.categoryService.loadCategories()
    .subscribe({
      error: (error) => {
        this.error.set("Something went wrong with fetching places");
      }
    });

    console.log("Xd")
    console.log(this.categoryService.categories);

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });

  }
}
