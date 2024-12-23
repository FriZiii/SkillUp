import { Component, inject, input, OnInit, signal } from '@angular/core';
import { CourseRatingComponent } from "./course-rating/course-rating.component";
import { CourseDetailRating, UserRatingDetail } from '../../../models/rating.model';
import { CourseRatingService } from '../../../services/course-rating.service';
import { InputText, InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-course-ratings-list',
  standalone: true,
  imports: [CourseRatingComponent, InputTextModule, SelectModule, FormsModule],
  templateUrl: './course-ratings-list.component.html',
  styleUrl: './course-ratings-list.component.css'
})
export class CourseRatingsListComponent implements OnInit{
  courseId = input.required<string>();

  ratingService = inject(CourseRatingService);
  courseRating: CourseDetailRating | undefined = undefined;
  filteredRatings: UserRatingDetail[] = [];
  searchText = '';
  selectedStars = signal(0);
  
  ngOnInit(){
    
    this.ratingService.getCourseDetailRating(this.courseId(), 100).subscribe((res) => {
      this.courseRating = res;
      this.filteredRatings = res.userRatings;
     })
  }

  starsOptions = [
    {name: '5 stars', value: 5},
    {name: '4 stars and more ', value: 4},
    {name: '3 stars and more', value: 3},
    {name: '2 stars and more', value: 2},
    {name: 'All', value: -0.5},
  ]

   applyFilters() {
    this.filteredRatings = this.courseRating!.userRatings!
        .filter(rating => {
          const matchesSearch = rating.feedback?.toLowerCase().includes(this.searchText.toLowerCase());
          const matchesStars = rating.stars >= this.selectedStars();
          return matchesSearch && matchesStars;
        });
    }
}
