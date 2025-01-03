import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CoursePercentage } from '../models/user-progress.model';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserProgressService {
  private httpClient = inject(HttpClient);
  public accomplishedElements = signal<string[]>([]);
  private accomplishedSubject = new BehaviorSubject<string[]>([]);
  public accomplished$: Observable<string[]> = this.accomplishedSubject.asObservable();
  public percentages = signal<CoursePercentage[]>([]);

  public addProgress(courseId: string, elementId: string) {
    return this.httpClient.post(environment.apiUrl + '/Courses/' + courseId + '/Elements/' + elementId + '/Progress', {}).pipe(
      tap(() => this.accomplishedElements.update(elements => [...elements, elementId]))
    );
  }

  public deleteProgress(courseId: string, elementId: string) {
    return this.httpClient.delete(environment.apiUrl + '/Courses/' + courseId + '/Elements/' + elementId + '/Progress').pipe(
      tap(() => this.accomplishedElements.update(elements => elements.filter(id => id !== elementId)))
    );
  }

  public getPercentage() {
    return this.httpClient.get<CoursePercentage[]>(
      environment.apiUrl + '/Courses/Progress'
    ).pipe(
      tap((res) => this.percentages.set(res))
    );
  }

  public getAcomplishedElementsForCourse(courseId: string) {
    return this.httpClient.get<string[]>(
      environment.apiUrl + '/Courses/' + courseId + '/Progress'
    ).pipe(
      tap((res) => {
        this.accomplishedSubject.next(res);
      })
    )
  }
}
