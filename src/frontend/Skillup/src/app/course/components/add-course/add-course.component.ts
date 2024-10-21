import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-course',
  standalone: true,
  imports: [],
  providers: [CategoryService],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css'
})
export class AddCourseComponent implements OnInit {
  /* categoryService = inject(CategoryService);
  categories = this.categoryService.categories;
  private destroyRef = inject(DestroyRef);
  error = signal('');

  ngOnInit(){
    const subscription = this.categoryService.loadCategories()
    .subscribe({
      next: () => {
        console.log(this.categoryService.categories());
      },
      error: (error) => { console.log("error"); },
      complete: () => { console.log(this.categories()); },
    });
    console.log("Xd")
    console.log(this.categoryService.categories());
    console.log(this.categories());
    this.destroyRef.onDestroy(() => {subscription.unsubscribe(); }); 
  }*/
  private httpClient = inject(HttpClient);
  categoryService = inject(CategoryService);
  categories: Category[] = [];
  cat = this.categoryService.categories;
  
  ngOnInit(){
    this.categoryService.loadCategories().subscribe({
      next: (resData) => {
        this.categories = resData;
        console.log(resData);
      }
    });
    console.log(this.cat());
  }
}
