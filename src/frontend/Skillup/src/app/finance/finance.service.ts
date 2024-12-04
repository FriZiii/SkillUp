import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { ToastHandlerService } from '../core/services/toast-handler.service';
import { Item } from './finance.model';

@Injectable({ providedIn: 'root' })
export class FinanceService {
  private toastService = inject(ToastHandlerService);
  private httpClient = inject(HttpClient);
  public items = signal<Item[]>([]);

  private itemSubject = new BehaviorSubject<Item[]>([]);
  private items$: Observable<Item[]> = this.itemSubject.asObservable();

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
          return throwError(() => error);
        })
      )
      .subscribe();
  }
}
