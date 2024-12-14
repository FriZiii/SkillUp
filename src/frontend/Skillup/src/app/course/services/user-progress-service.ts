import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CoursePercentage } from '../models/user-progress.model';

@Injectable({ providedIn: 'root' })
export class UserProgressService {
  private httpClient = inject(HttpClient);
  public accomplishedElements = signal<string[]>([]);

  public addProgress(courseId: string, elementId: string) {
    return this.httpClient.post(
      environment.apiUrl +
        '/Courses/' +
        courseId +
        '/Elements/' +
        elementId +
        '/Progress',
      {}
    );
  }

  public deleteProgress(courseId: string, elementId: string) {
    return this.httpClient.delete(
      environment.apiUrl +
        '/Courses/' +
        courseId +
        '/Elements/' +
        elementId +
        '/Progress'
    );
  }

  public getPercentage() {
    return this.httpClient.get<CoursePercentage[]>(
      environment.apiUrl + '/Courses/Progress'
    );
  }

  public getAcomplishedElementsForCourse(courseId: string) {
    return this.httpClient.get<string[]>(
      environment.apiUrl + '/Courses/' + courseId + '/Progress'
    );
  }
}
