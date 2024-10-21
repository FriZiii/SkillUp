import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-add-course',
  standalone: true,
  imports: [],
  providers: [CategoryService],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css',
})
export class AddCourseComponent implements OnInit {
  categories: Category[] = [];

  constructor(private courseCategoryService: CategoryService) {}

  ngOnInit(): void {
    this.courseCategoryService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }
}
