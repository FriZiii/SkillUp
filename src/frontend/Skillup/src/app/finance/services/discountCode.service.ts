import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { AddDiscountCode, DiscountCode, DiscountCodeType } from "../models/discountCodes.model";

@Injectable({ providedIn: 'root' })
export class DiscountCodeService {
  private httpClient = inject(HttpClient);

  public addDiscountCode(type: DiscountCodeType, addDiscountCode: AddDiscountCode){
    return this.httpClient
      .post<DiscountCode>(environment.apiUrl + '/Finances/DiscountCode/' + type , {addDiscountCode});
  }

  public getDiscountCodesByOwner(userId: string){
    return this.httpClient
      .get<DiscountCode[]>(environment.apiUrl + '/Finances/DiscountCode/' + userId);
  }

  public editDiscountCode(codeId: string, addDiscountCode: AddDiscountCode){
    return this.httpClient
      .put<DiscountCode>(environment.apiUrl + '/Finances/DiscountCode/' + codeId, {addDiscountCode});
  }

  public deleteDiscountCode(codeId: string){
    return this.httpClient
      .delete(environment.apiUrl + '/Finances/DiscountCode/' + codeId);
  }
}