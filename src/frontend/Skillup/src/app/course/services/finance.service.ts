import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Category } from '../models/category.model';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ToastHandlerService } from '../../core/services/ToastHandlerService';
import { Item, ItemType } from '../models/finance.model';

@Injectable({ providedIn: 'root' })
export class FinanceService {
  toastService = inject(ToastHandlerService);
  private httpClient = inject(HttpClient);
  public items = signal<Item[]>([]);

  private itemSubject = new BehaviorSubject<Item[]>([]);
  public items$: Observable<Item[]> =
    this.itemSubject.asObservable();

  constructor() {
    this.fetchItems();
    this.items$.subscribe((data) => {
      this.items.set(data);
    });
  }

  private fetchItems() {
    this.httpClient
      .get<any>(environment.apiUrl + '/Finances/Items?itemType=Course')
      .pipe(
        tap((items) => {
          this.itemSubject.next(items);
        }),
        catchError((error) => {
          this.toastService.showErrorToast("Coud not fetch prices")
          return throwError(() => error);
        })
      )
      .subscribe();
  }
}
