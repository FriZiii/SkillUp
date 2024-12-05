import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { Cart } from "../models/cart.model";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CartService
{
    private httpClient = inject(HttpClient);
    public cart = signal<Cart | null>(null);

  constructor(){
    const cartId = localStorage.getItem('cartId');
    if(cartId){
      this.getCart(cartId).subscribe((res) => 
        {
          this.cart.set(res);
          console.log(this.cart());
        }
      )
    }
  }

    addToCart(itemId: string) {
        console.log('add');
      const cartId = localStorage.getItem('cartId');
      var body
      if(cartId !== null){
        body = '?cartId=' + cartId + '&itemId=' + itemId;
      }
      else{
        body = '?itemId=' + itemId;
      }
    
      return this.httpClient
      .post<Cart>(environment.apiUrl + '/Finances/Cart/Items' + body, {})
        .pipe(
          tap((response) => {
            localStorage.setItem('cartId', response.id);
            this.cart.set(response)
          })
        );
    }

    private getCart(cartId: string){
        return this.httpClient
        .get<any>(environment.apiUrl + '/Finances/Cart/' + cartId)
        .pipe(
        );
      
    }
}