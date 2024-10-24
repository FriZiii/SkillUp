import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Category } from '../models/category.model';
import { BehaviorSubject, catchError, delay, Observable, tap, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private httpClient = inject(HttpClient);

  private categoriesSubject = new BehaviorSubject<Category[]>([]);
  public categories$: Observable<Category[]> = this.categoriesSubject.asObservable();

  constructor() {
    this.fetchCategories();
  }

  getCategories(): Observable<any[]> {
    return this.categories$;
  }

  private fetchCategories() {
    this.httpClient
      .get<Category[]>(environment.apiUrl + '/Courses/Categories')
      .pipe(
        delay(3000),
        tap((categories) => this.categoriesSubject.next(categories)),
        catchError(error => {return throwError(() => error)}))
      .subscribe();
  }
}