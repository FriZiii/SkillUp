import { inject, Injectable, signal } from "@angular/core";
import { Course } from "../models/course.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs";

@Injectable({ providedIn: 'root' })
export class PurchasedItemsService {
    public purchasedCourses = signal<Course[]>([]);
    private httpClient = inject(HttpClient);

    getPurchasedCourses(userId: string){
        this.httpClient
          .get<Course[]>(environment.apiUrl + '/Courses/UserId/' +  userId)
          .pipe(
            tap((res) => {
              //this.purchasedCourses.set(res.map((course) => this.mapCourseToCourseItem(course)))
              this.purchasedCourses.set(res)
            })
          ).subscribe();
      }}