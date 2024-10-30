import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { AddCourse, CourseDetail, Course } from "../models/course.model";
import { environment } from "../../../environments/environment";
import { BehaviorSubject, catchError, Observable, tap, throwError } from "rxjs";
import { ToastHandlerService } from "../../core/services/ToastHandlerService";


@Injectable({ providedIn: 'root' })
export class CoursesService {
    private httpClient = inject(HttpClient);
    toastService = inject(ToastHandlerService);
    private coursesSubject = new BehaviorSubject<Course[]>([]);
    courses$: Observable<Course[]> = this.coursesSubject.asObservable();
    public courses = signal<Course[]>([]);

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
            tap((courses) => {
                this.coursesSubject.next(courses)
            }),
            catchError(error => {
                this.toastService.showErrorToast("Coud not fetch courses");
                return throwError(() => error)
            }))
            .subscribe();
    }

    fetchCourseById(courseId: string){
        return this.httpClient
        .get<any>(environment.apiUrl + '/Courses/' + courseId)
        .pipe(
            tap((courses) => {
                //this.coursesSubject.next(courses)
            }),
            catchError(error => {
                this.toastService.showErrorToast("Coud not fetch course");
                return throwError(() => error)
            }))
    }

}