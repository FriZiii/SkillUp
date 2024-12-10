import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CourseDetail } from "../models/course.model";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs";
import { CoursesService } from "./course.service";
import { CourseStatus } from "../models/course-status.model";
import { Review, ReviewStatus } from "../models/review.model";

@Injectable({ providedIn: 'root' })
export class CourseReviewService {
    private httpClient = inject(HttpClient);
    private courseService = inject(CoursesService);

    public submitToReview(courseId: string){
        return this.httpClient.patch<CourseDetail>(
            environment.apiUrl + '/Courses/' + courseId + '/Submit', {}).pipe(
                tap((response) => 
                    this.courseService.courses.update((courses) => 
                        courses.map((course) => 
                        course.id === courseId ? {...course, status: CourseStatus.SubmitedForReview } : course
                ) ))
            );
    }

    public getReviewWithStatus(status: ReviewStatus){
        return this.httpClient.get<Review[]>(environment.apiUrl + '/Courses/Review?reviewStatus=' + status);
    }

    public startReview(courseId: string){
        return this.httpClient.post<Review>(environment.apiUrl + '/Courses/' + courseId + '/Reviews', {});
    }
}