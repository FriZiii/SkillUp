import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Order } from '../models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private httpClient = inject(HttpClient);

  public getOrderByBalanceHistoryId(id: string) {
    return this.httpClient.get<Order>(
      environment.apiUrl + '/Finances/Order/?balanceHistoryId=' + id
    );
  }
}
