import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import {
  AddDiscountCode,
  DiscountCode,
  DiscountCodeType,
} from '../models/discountCodes.model';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DiscountCodeService {
  private httpClient = inject(HttpClient);
  public publicDiscountCodes = signal<DiscountCode[]>([]);
  private discountCodesSubject = new BehaviorSubject<DiscountCode[]>([]);
  private discountCodes$: Observable<DiscountCode[]> = this.discountCodesSubject.asObservable();

  constructor() {
    this.fetchPublicDiscountCodes();
    this.discountCodes$.subscribe((data) => {
      this.publicDiscountCodes.set(data);
    });
  }

  private fetchPublicDiscountCodes(){
    this.httpClient.get<DiscountCode[]>(
      environment.apiUrl + '/Finances/DiscountCode'
    ).pipe(
      tap((result: any) => {this.discountCodesSubject.next(result)})
    ).subscribe();
  }
  public addDiscountCode(
    type: DiscountCodeType,
    addDiscountCode: AddDiscountCode
  ) {
    return this.httpClient.post<DiscountCode>(
      environment.apiUrl + '/Finances/DiscountCode/' + type,
      addDiscountCode
    );
  }

  public getDiscountCodesByOwner(userId: string) {
    return this.httpClient.get<DiscountCode[]>(
      environment.apiUrl + '/Finances/DiscountCode/' + userId
    );
  }

  public editDiscountCode(codeId: string, addDiscountCode: AddDiscountCode) {
    return this.httpClient.put<DiscountCode>(
      environment.apiUrl + '/Finances/DiscountCode/' + codeId,
      addDiscountCode
    );
  }

  public deleteDiscountCode(codeId: string) {
    return this.httpClient.delete(
      environment.apiUrl + '/Finances/DiscountCode/' + codeId
    );
  }

  public toggleDiscountCodeItem(codeId: string, itemId: string){
    return this.httpClient.post<DiscountCode>(
      environment.apiUrl + '/Finances/DiscountCode/' + codeId + '/' + itemId, {}
    );
  }

  findDiscountCodesByItemId(itemId: string): DiscountCode[] {
    return this.publicDiscountCodes().filter(dc => 
        dc.discountedItems?.some(item => item.id === itemId)
    );
}
}
