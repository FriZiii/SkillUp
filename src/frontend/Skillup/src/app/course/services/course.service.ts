import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { AddCourse, CourseDetail, CourseListItem } from "../models/course.model";
import { environment } from "../../../environments/environment";
import { BehaviorSubject, catchError, Observable, tap, throwError } from "rxjs";


@Injectable({ providedIn: 'root' })
export class CoursesService {
    private httpClient = inject(HttpClient);
    private coursesSubject = new BehaviorSubject<CourseListItem[]>([]);
    courses$: Observable<CourseListItem[]> = this.coursesSubject.asObservable();
    public courses = signal<CourseListItem[]>([]);

    constructor(){
        this.fetchCourses();
        this.courses$.subscribe((data) => {
            this.courses.set(data);
        });
    }

    addCourse(courseData: AddCourse){
        return this.httpClient
        .post<any>(environment.apiUrl + '/Courses', courseData)
        .pipe(
            catchError(error => {return throwError(() => error)}),
            tap((response: CourseDetail) => {
                console.log(response);
                this.courses.set([...this.courses(), {
                    id: response.id,
                    title: response.title,
                    isPublished: response.isPublished,
                    category: {
                        id: response.category.id,
                        name: response.category.name,
                        slug: response.category.slug,
                        subcategory: {
                            id: response.category.subcategory.id,
                            name: response.category.subcategory.name,
                            slug: response.category.subcategory.slug,
                        },
                    },
                    thumbnailUrl: response.thumbnailUrl
                }])
                console.log(this.courses());
            })
        );
    }

    getCourses(){
        return this.courses$;
    }

    private fetchCourses(){
        console.log('fetch courses');
        this.httpClient
        .get<any>(environment.apiUrl + '/Courses')
        .pipe(
            tap((courses) => this.coursesSubject.next(courses)),
            catchError(error => {return throwError(() => error)}))
            .subscribe();
    }

}