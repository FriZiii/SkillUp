import { HttpClient } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import {
  AddCourse,
  CourseDetail,
  Course,
  CourseListItem,
} from '../models/course.model';
import { environment } from '../../../environments/environment';
import {
  BehaviorSubject,
  map,
  Observable,
  of,
  tap,
} from 'rxjs';
import { FinanceService } from '../../finance/services/finance.service';
import { CourseLevel } from '../models/course-level.model';
import { CourseStatus } from '../models/course-status.model';
import { CourseRatingService } from './course-rating.service';

@Injectable({ providedIn: 'root' })
export class CoursesService {
  //Services
  private financeService = inject(FinanceService);
  private ratingService = inject(CourseRatingService);

  //Variables
  private httpClient = inject(HttpClient);
  private coursesSubject = new BehaviorSubject<Course[]>([]);
  public courses$: Observable<Course[]> = this.coursesSubject.asObservable();
  public courses = signal<CourseListItem[]>([]);    
  public publishedCourses = computed(() => this.courses().filter(c => c.status === CourseStatus.Published)); 

  private items = this.financeService.items;
  private ratings = this.ratingService.ratings;

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
        tap((response: any) => {
          this.courses.set([
            ...this.courses(),
            {
              id: response.id,
              title: response.title,
              authorId: response.authorId,
              authorName:  response.authorName,
              status: response.status,
              usersCount: 0,
              level: CourseLevel.None,
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
              price:  0,
              averageRating: 0,
              ratingsCount: 0
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
      )
      .subscribe();
  }

  getCourseDetailById(courseId: string): Observable<CourseDetail> {
    const item = this.courses().find((item) => item.id === courseId);
    return this.fetchCourseById(courseId).pipe(
      map((res) => {
        const courseWithPrice = {
          ...res,
          price:  item?.price ?? 0,
        };
        return courseWithPrice;
      })
    );
  }

  getCourseByAuthorId(authorId: string){
    return this.httpClient
      .get<any>(environment.apiUrl + '/Courses/Author/' + authorId);
  }

  private fetchCourseById(courseId: string) {
    return this.httpClient
      .get<any>(environment.apiUrl + '/Courses/' + courseId);
  }

  mapCourseToCourseItem(course: Course): CourseListItem {
    const item = this.items().find((item) => item.id === course.id);
    const rating = this.ratings().find((rating) => rating.courseId === course.id);
    return {
      ...course,
      price:  item?.price ?? 0,
      averageRating: rating?.averageStars ?? 0,
      ratingsCount: rating?.ratingsCount ?? 0
    };
  }

  mapCourseItemToCourse(courseItem: CourseListItem): Course {
    return {
      ...courseItem
    }
  }

  getCouresByCategoryId(categoryId: string): CourseListItem[] {
    return this.publishedCourses().filter((course) => course.category.id === categoryId);
  }

  getCoursesBySlug(category: string, subcategory: string) {
    return of(this.publishedCourses().filter(
      (course) =>
        course.category.slug === category &&
        (subcategory.toLowerCase() === 'all' ||
          course.category.subcategory.slug === subcategory)
    ));
  }

  getCoursesByAuthor(authorId: string): CourseListItem[]{
    return this.publishedCourses().filter((course) => course.authorId === authorId);
  }

  getCourseById(id: string){
    return this.courses().filter((course) => course.id === id);
  }


  //Edit
  editCourse(courseId: string, title: string, categoryId: string, subcategoryId: string){
    return this.httpClient
    .put<Course>(environment.apiUrl + '/Courses/' + courseId, {title: title, categoryId: categoryId, subcategoryId: subcategoryId})
    .pipe(
      tap((response) => {
        this.courses.update((prevCourses) => 
        prevCourses.map((course) => course.id === courseId ? {...course, title: title, category: {
          id: response.category.id,
          name: response.category.name,
          slug: response.category.slug,
                subcategory: {
                  id: response.category.subcategory.id,
                  name: response.category.subcategory.name,
                  slug: response.category.subcategory.slug,
                },
        }} : course));
      })
    );
  }

  editCourseDetails(courseId: string, subtitle: string, description: string, level: CourseLevel, objectivesSummary: string[], mustKnowBefore: string[], intededFor: string[]){
    return this.httpClient
        .put(environment.apiUrl + '/Courses/' + courseId + '/Details', {subtitle: subtitle, description: description, level: level, objectivesSummary: objectivesSummary, mustKnowBefore: mustKnowBefore, intendedFor: intededFor})
        .pipe(
          tap(() => {
            this.courses.update((prevCourses) => 
             prevCourses.map(course => course.id === courseId ? {...course, subtitle: subtitle, description: description, level: level, objectivesSummary: objectivesSummary, mustKnowBefore: mustKnowBefore, intendedFor: intededFor} : course));
          })
        );

  }

  editCourseThumbnailPicture(courseId: string, newPicture: File){
    const formData = new FormData(); 
    formData.append('file', newPicture);
    return this.httpClient
        .put<any>(environment.apiUrl + '/Courses/' + courseId + '/Details/TumbnailPicture', formData)
        .pipe(
          tap((res: any) => {
            this.courses.update((prevCourses) => 
             prevCourses.map(course => course.id === courseId ? {...course, thumbnailUrl: res.thumbnailUrl} : course));
          })
        );
  }
}
