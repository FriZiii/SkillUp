import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { AddCourse } from "../models/course.model";
import { environment } from "../../../environments/environment";
import { catchError, tap, throwError } from "rxjs";


@Injectable({ providedIn: 'root' })
export class CoursesService {
    private httpClient = inject(HttpClient);

    addCourse(courseData: AddCourse){
        console.log("prawieee");
        return this.httpClient
        .post<any>(environment.apiUrl + '/Courses', courseData)
        .pipe(
            catchError(error => {return throwError(() => error)}),
            tap((response: any) => {
                console.log(response);
            })
        );

    }
}