import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Category } from '../models/category.model';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ToastHandlerService } from '../../core/services/toast-handler.service';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private toastService = inject(ToastHandlerService);
  private httpClient = inject(HttpClient);
  public categories = signal<Category[]>([]);

  private categoriesSubject = new BehaviorSubject<Category[]>([]);
  private categories$: Observable<Category[]> =
    this.categoriesSubject.asObservable();

  constructor() {
    this.fetchCategories();
    this.categories$.subscribe((data) => {
      this.categories.set(data);
    });
  }

  private fetchCategories() {
    this.httpClient
      .get<Category[]>(environment.apiUrl + '/Courses/Categories')
      .pipe(
        tap((categories) => {
          this.categoriesSubject.next(categories);
        }),
        catchError((error) => {
          this.toastService.showError('Coud not fetch categories');
          return throwError(() => error);
        })
      )
      .subscribe();
  }
}
