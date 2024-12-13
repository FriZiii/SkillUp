import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserRating } from "../models/rating.model";
import { environment } from "../../../environments/environment";

@Injectable({ providedIn: 'root' })
export class CourseRatingService {
    private httpClient = inject(HttpClient);

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
}