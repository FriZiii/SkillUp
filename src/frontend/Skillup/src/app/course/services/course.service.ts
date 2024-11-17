import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import {
  AddCourse,
  CourseDetail,
  Course,
  CourseListItem,
} from '../models/course.model';
import { environment } from '../../../environments/environment';
import {
  BehaviorSubject,
  catchError,
  map,
  Observable,
  tap,
  throwError,
} from 'rxjs';
import { ToastHandlerService } from '../../core/services/toast-handler.service';
import { FinanceService } from '../../finance/finance.service';

@Injectable({ providedIn: 'root' })
export class CoursesService {
  //Services
  private financeService = inject(FinanceService);
  private toastService = inject(ToastHandlerService);

  //Variables
  private httpClient = inject(HttpClient);
  private coursesSubject = new BehaviorSubject<Course[]>([]);
  private courses$: Observable<Course[]> = this.coursesSubject.asObservable();
  public courses = signal<CourseListItem[]>([]);

  private items = this.financeService.items;

  constructor() {
    this.fetchCourses();
    this.courses$.subscribe((data) => {
      this.courses.set(
        data.map((course) => this.mapCourseToCourseItem(course))
      );
    });
  }

  addCourse(courseData: AddCourse) {
    return this.httpClient
      .post<any>(environment.apiUrl + '/Courses', courseData)
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        }),
        tap((response: any) => {
          console.log(response);
          this.courses.set([
            ...this.courses(),
            {
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
              thumbnailUrl: response.thumbnailUrl,
              price: {
                amount: 0,
              },
            },
          ]);
        })
      );
  }

  getCourses() {
    return this.courses$;
  }

  private fetchCourses() {
    this.httpClient
      .get<any>(environment.apiUrl + '/Courses')
      .pipe(
        tap((courses) => {
          this.coursesSubject.next(courses);
        }),
        catchError((error) => {
          return throwError(() => error);
        })
      )
      .subscribe();
  }

  getCourseById(courseId: string): Observable<CourseDetail> {
    const item = this.courses().find((item) => item.id === courseId);
    return this.fetchCourseById(courseId).pipe(
      map((res) => {
        const courseWithPrice = {
          ...res,
          price: {
            amount: item?.price.amount ?? 0,
          },
        };
        return courseWithPrice;
      })
    );
  }

  private fetchCourseById(courseId: string) {
    return this.httpClient
      .get<any>(environment.apiUrl + '/Courses/' + courseId)
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  private mapCourseToCourseItem(course: Course): CourseListItem {
    const item = this.items().find((item) => item.id === course.id);
    return {
      ...course,
      price: {
        amount: item?.price.amount ?? 0,
      },
    };
  }

  getCouresByCategoryId(categoryId: string): CourseListItem[] {
    return this.courses().filter((course) => course.category.id === categoryId);
  }

  getCoursesBySlug(category: string, subcategory: string): CourseListItem[] {
    return this.courses().filter(
      (course) =>
        course.category.slug === category &&
        (subcategory.toLowerCase() === 'all' ||
          course.category.subcategory.slug === subcategory)
    );
  }
}
