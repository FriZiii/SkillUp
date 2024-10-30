import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { AddCourse, CourseDetail, Course, CourseListItem } from "../models/course.model";
import { environment } from "../../../environments/environment";
import { BehaviorSubject, catchError, Observable, tap, throwError } from "rxjs";
import { ToastHandlerService } from "../../core/services/toasthandler.service";
import { FinanceService } from "../../finance/finance.service";


@Injectable({ providedIn: 'root' })
export class CoursesService {
    //Services
    financeService = inject(FinanceService)
    toastService = inject(ToastHandlerService);

    //Variables
    private httpClient = inject(HttpClient);
    private coursesSubject = new BehaviorSubject<Course[]>([]);
    courses$: Observable<Course[]> = this.coursesSubject.asObservable();
    public courses = signal<Course[]>([]);
    public coursesListItem = signal<CourseListItem[]>([]);

    constructor(){
        this.fetchCourses();
        this.courses$.subscribe((data) => {
            this.courses.set(data);
            this.coursesListItem.set(data.map(course => this.mapCourseToCourseItem(course)));
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
                this.toastService.showError("Coud not fetch courses");
                return throwError(() => error)
            }))
            .subscribe();
    }

    fetchCourseById(courseId: string){
        return this.httpClient
        .get<any>(environment.apiUrl + '/Courses/' + courseId)
        .pipe(
            catchError(error => {
                this.toastService.showError("Coud not fetch course");
                return throwError(() => error)
            }))
    }

    mapCourseToCourseItem(course: Course): CourseListItem { 
        const items = this.financeService.items;
        const item = items().find(item => item.id === course.id);
        return {
          ...course,
          price: {
            amount: item?.price.amount ?? 0,
          },
        };
      }

    getCouresByCategoryId(categoryId: string) : CourseListItem[]{
        return this.courses().filter(course => course.category.id === categoryId).map(course => this.mapCourseToCourseItem(course));
    }

    getCoursesBySlug(category: string, subcategory: string): CourseListItem[]{
        return this.courses()
        .filter(course => course.category.slug === category && (subcategory.toLowerCase() === 'all' || course.category.subcategory.slug === subcategory))
        .map(course => this.mapCourseToCourseItem(course));
    }
}