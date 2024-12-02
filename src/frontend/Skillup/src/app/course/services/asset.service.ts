import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { environment } from "../../../environments/environment";
import { catchError, map, Observable, tap, throwError } from "rxjs";
import { ToastHandlerService } from "../../core/services/toast-handler.service";
import { AssetType, Section } from "../models/course-content.model";
import { ExerciseType } from "../models/exercise.model";

@Injectable({ providedIn: 'root' })
export class AssetService {
    private httpClient = inject(HttpClient);

    addArticle(elementId: string, articleFile: File) {
        const formData = new FormData();
        formData.append('file', articleFile);
      return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Assets/article/' + elementId, formData)
        .pipe(
          tap((response) => {})
        );
    }

    addVideo(elementId: string, articleFile: File) {
        const formData = new FormData(); 
        formData.append('file', articleFile);

        return this.httpClient
          .post<any>(environment.apiUrl + '/Courses/Assets/video/' + elementId, formData)
          .pipe(
            tap((response) => {})
          );
      }

      addAssignment(elementId: string, exerciseType: ExerciseType, instruction: string) {
        return this.httpClient
          .post<any>(environment.apiUrl + '/Courses/Assets/assignment/'+ exerciseType + '/' + elementId, {instruction: instruction})
          .pipe(
            catchError((error) => {
              return throwError(() => error);
            }),
            tap((response) => {console.log(response)})
          );
      }

    getAsset(elementId: string, assetType: AssetType) {
        return this.httpClient
      .get<any>(environment.apiUrl + '/Courses/Assets/' + assetType + '/' + elementId)
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
      }

    deleteAsset(elementId: string){
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Assets/' + elementId)
        .pipe(
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }

}