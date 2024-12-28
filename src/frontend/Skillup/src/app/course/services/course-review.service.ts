import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
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
    public allReviewsForCourse = signal<Review[] | null>(null);
    public latestReviewForCourse = signal<Review | null>(null);

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

    public getReviewsByCourse(courseId: string){
        return this.httpClient.get<Review[]>(environment.apiUrl + '/Courses/' + courseId + '/Reviews');
    }

    public getLatestReviewByCourse(courseId: string){
        return this.httpClient.get<Review>(environment.apiUrl + '/Courses/' + courseId + '/Reviews/Latest');
    }

    public addComment(reviewId: string, courseId: string, elementId: string | null, comment: string){
        if(elementId){
            return this.httpClient.post<Review>(environment.apiUrl + '/Courses/Review/' + reviewId + '/Comments?courseId=' + courseId + '&elementId=' + elementId + '&comment=' + comment, {});
        }
        else{
            return this.httpClient.post<Review>(environment.apiUrl + '/Courses/Review/' + reviewId + '/Comments?courseId=' + courseId + '&comment=' + comment, {});
        }
    }

    public deleteComment(commentId: string){
        return this.httpClient.delete(environment.apiUrl + '/Courses/Review/Comments/' + commentId);
    }

    public finalizeReview(reviewId: string, status: ReviewStatus, courseId: string){
        return this.httpClient.patch(
            environment.apiUrl + '/Courses/Review/' + reviewId + '?reviewStatus=' + status, {}).pipe(
                tap((response) => 
                {
                    if(status === ReviewStatus.FinalizedWithRequiredChanges){
                        this.courseService.courses.update((courses) => 
                            courses.map((course) => 
                            course.id === courseId ? {...course, status: CourseStatus.ChangesRequired } : course
                    ) )
                }
                    if(status === ReviewStatus.Finalized){
                        this.courseService.courses.update((courses) => 
                            courses.map((course) => 
                            course.id === courseId ? {...course, status: CourseStatus.Published } : course
                    ) )
                    }

                })
            );
    }

    public resolveComment(commentId: string){
        return this.httpClient.patch<Review>(environment.apiUrl + '/Courses/Review/Comments/' + commentId + '/Resolve', {});
    }
}