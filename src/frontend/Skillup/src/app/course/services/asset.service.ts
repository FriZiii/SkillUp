import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { AssetType } from "../models/course-content.model";
import { ExerciseType } from "../models/exercise.model";

@Injectable({ providedIn: 'root' })
export class AssetService {
    private httpClient = inject(HttpClient);

    addArticle(elementId: string, articleFile: File) {
        const formData = new FormData();
        formData.append('file', articleFile);
      return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Assets/article/' + elementId, formData);
    }

    addVideo(elementId: string, articleFile: File) {
        const formData = new FormData(); 
        formData.append('file', articleFile);

        return this.httpClient
          .post<any>(environment.apiUrl + '/Courses/Assets/video/' + elementId, formData);
      }

      addAssignment(elementId: string, exerciseType: ExerciseType, instruction: string) {
        return this.httpClient
          .post<any>(environment.apiUrl + '/Courses/Assets/assignment/'+ exerciseType + '/' + elementId, {instruction: instruction});
      }

      editAssignment(elementId: string, instruction: string) {
        return this.httpClient
          .put<any>(environment.apiUrl + '/Courses/Assets/assignment/' + elementId, {instruction: instruction});
      }

    getAsset(elementId: string, assetType: AssetType) {
        return this.httpClient
      .get<any>(environment.apiUrl + '/Courses/Assets/' + assetType + '/' + elementId);
      }

    deleteAsset(elementId: string){
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Assets/' + elementId);
    }

}