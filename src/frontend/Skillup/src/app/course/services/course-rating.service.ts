import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { AverageRating, CourseDetailRating, UserRating } from "../models/rating.model";
import { environment } from "../../../environments/environment";
import { BehaviorSubject, Observable, tap } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CourseRatingService {
    private httpClient = inject(HttpClient);
    public ratings = signal<AverageRating[]>([]);
    
    private ratingsSubject = new BehaviorSubject<AverageRating[]>([]);
    private ratings$: Observable<AverageRating[]> = this.ratingsSubject.asObservable();
    
    constructor() {
        this.fetchRating();
        this.ratings$.subscribe((data) => {
          this.ratings.set(data);
        });
    }

    private fetchRating() {
        this.httpClient
          .get<any>(environment.apiUrl + '/Courses/Ratings')
          .pipe(
            tap((ratings) => {
              this.ratingsSubject.next(ratings);
            })
          )
          .subscribe();
    }

    public addRating(courseId: string, stars: number, feedback: string){
        return this.httpClient.post<UserRating>(environment.apiUrl + '/Courses/' + courseId + '/Ratings', {stars: stars, feedback: feedback });
    }

    public getUserRatings(userId: string){
        return this.httpClient.get<UserRating[]>(environment.apiUrl + '/Courses/Ratings/' + userId);
    }

    public editRating(ratingId: string, stars: number, feedback: string){
        return this.httpClient.patch<UserRating>(environment.apiUrl + '/Courses/Ratings/' + ratingId, {stars: stars, feedback: feedback });
    }

    public deleteRating(ratingId: string){
        return this.httpClient.delete(environment.apiUrl + '/Courses/Ratings/' + ratingId);
    }

    public getCourseDetailRating(courseId: string, numOfRatings: number){
        return this.httpClient.get<CourseDetailRating>(environment.apiUrl + '/Courses/'+ courseId + '/Ratings?itemType=' + numOfRatings);
    }
}