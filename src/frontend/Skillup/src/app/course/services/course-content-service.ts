import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { catchError, map, Observable, tap, throwError } from "rxjs";
import { ToastHandlerService } from "../../core/services/toast-handler.service";
import { ElementType, Section } from "../models/course-content.model";

@Injectable({ providedIn: 'root' })
export class CourseContentService {
    private toastService = inject(ToastHandlerService);
    private httpClient = inject(HttpClient);

    getSectionsByCourseId(courseId: string): Observable<Section[]> {
        return this.fetchSectionsByCourseId(courseId);
      }

    private fetchSectionsByCourseId(courseId: string) {
        return this.httpClient
          .get<any>(environment.apiUrl + '/Courses/Sections/' + courseId)
          .pipe(
            map((response) => {
              response.elementType = response.elementType as ElementType;
              console.log(response);
              return response;
            }),
            catchError((error) => {
              this.toastService.showError('Coud not fetch sections');
              return throwError(() => error);
            })
          );
    }

    addSection(sectionTitle: string, courseId: string) {
      return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Sections/' + courseId, {title: sectionTitle})
        .pipe(
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }
}