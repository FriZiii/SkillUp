import { inject, Injectable, signal } from "@angular/core";
import { Course } from "../models/course.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { catchError, tap, throwError } from "rxjs";

@Injectable({ providedIn: 'root' })
export class PurchasedItemsService {
    public purchasedCourses = signal<Course[]>([]);
    private httpClient = inject(HttpClient);

    getPurchasedCourses(userId: string){
        this.httpClient
          .get<Course[]>(environment.apiUrl + '/Courses/UserId/' +  userId)
          .pipe(
            tap((res) => {
              this.purchasedCourses.set(res)
            }),
            catchError((error) => {
              return throwError(() => error);
            })
          ).subscribe();
      }}