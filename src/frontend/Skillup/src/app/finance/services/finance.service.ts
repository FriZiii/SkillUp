import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Item } from '../models/finance.model';
import { CoursesService } from '../../course/services/course.service';

@Injectable({ providedIn: 'root' })
export class FinanceService {
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

  public editPrice(itemId: string, price: number){
    return this.httpClient.put(environment.apiUrl + '/Finances/Items/' + itemId, {currency: price}).pipe(
      tap((items) => {
        this.items.update((prevItems) => 
          prevItems.map(item => item.id === itemId ? {...item, price: price} : item)
        )
      })
    )
  }
}
