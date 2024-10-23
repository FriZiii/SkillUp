import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { AddCourse, CourseListItem } from "../models/course.model";
import { environment } from "../../../environments/environment";
import { BehaviorSubject, catchError, Observable, tap, throwError } from "rxjs";


@Injectable({ providedIn: 'root' })
export class CoursesService {
    private httpClient = inject(HttpClient);
    private coursesSubject = new BehaviorSubject<CourseListItem[]>([]);
    courses$: Observable<CourseListItem[]> = this.coursesSubject.asObservable();

    constructor(){
        this.fetchCourses();
    }

    addCourse(courseData: AddCourse){
        return this.httpClient
        .post<any>(environment.apiUrl + '/Courses', courseData)
        .pipe(
            catchError(error => {return throwError(() => error)}),
            tap((response: any) => {
                console.log(response);
            })
        );
    }

    getCourses(){
        return this.courses$;
    }

    private fetchCourses(){
        this.httpClient
        .get<any>(environment.apiUrl + '/Courses')
        .pipe(
            tap((courses) => this.coursesSubject.next(courses)),
            catchError(error => {return throwError(() => error)}))
            .subscribe();
    }

}