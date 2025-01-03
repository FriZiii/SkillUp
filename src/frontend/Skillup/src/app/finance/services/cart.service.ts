import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { Cart, CartItemForDisplay } from "../models/cart.model";
import { environment } from "../../../environments/environment";
import { catchError, tap, throwError } from "rxjs";
import { CoursesService } from "../../course/services/course.service";
import { WalletService } from "./wallet.service";
import { PurchasedItemsService } from "../../course/services/purchasedItems.service";
import { UserService } from "../../user/services/user.service";

@Injectable({ providedIn: 'root' })
export class CartService
{
    private httpClient = inject(HttpClient);
    private walletService = inject(WalletService);
    private courseService = inject(CoursesService);
    private purchasedItemsService = inject(PurchasedItemsService);
    private userService = inject(UserService);
    private cartId = localStorage.getItem('cartId');
    public cart = signal<Cart | null>(null);
    public cartItemsDisplay = signal<CartItemForDisplay[] | null>(null);

  constructor(){
    if(this.cartId){
      this.getCart(this.cartId).subscribe((res) => 
        {
          this.cart.set(res);
        }
      )
    }
  }

    addToCart(itemId: string) {
      var body
      if(this.cartId !== null){
        body = '?cartId=' + this.cartId + '&itemId=' + itemId;
      }
      else{
        body = '?itemId=' + itemId;
      }
    
      return this.httpClient
      .post<Cart>(environment.apiUrl + '/Finances/Cart/Items' + body, {})
        .pipe(
          tap((response) => {
            localStorage.setItem('cartId', response.id);
            this.cartId = response.id;
            this.cart.set(response);
          })
        );
    }


    deleteItemFromCart(itemId: string){
      const prevItems = this.cart()?.items;
      return this.httpClient
        .delete<Cart>(environment.apiUrl + '/Finances/Cart/' + this.cartId + '/Items/' + itemId)
        .pipe(
          tap((response) => {
            if(prevItems && prevItems.length > 1){
              /* this.cart.update((c) => {
                if(!c) return c;
                return {
                  ...c,
                  items: c.items.filter(i => i.id !== itemId)
                };
              }); */
              this.cart.set(response);
            }
            else{
              this.cart.set(null);
              this.cartItemsDisplay.set(null);
              localStorage.removeItem('cartId');
              this.cartId = null;
            }
          }),
           catchError((error) => {
            this.cart.set(null);
              this.cartItemsDisplay.set(null);
              localStorage.removeItem('cartId');
              this.cartId = null;
              return throwError(() => error);
          })
        );
      }


    toggleDiscountCodeForCart(discountCode: string){
      return this.httpClient
      .post<Cart>(environment.apiUrl + '/Finances/Cart/' + this.cartId + '/discount-code?discountCode=' + discountCode, {})
        .pipe(
          tap((response) => {
            localStorage.setItem('cartId', response.id);
            this.cartId = response.id;
            this.cart.set(response)
          })
        );
    }

    delCode(){
      return this.httpClient
      .post<Cart>(environment.apiUrl + '/Finances/Cart/' + this.cartId  + '/discount-code', {})
        .pipe(
          tap((response) => {
            localStorage.setItem('cartId', response.id);
            this.cartId = response.id;
            this.cart.set(response)
          })
        );
    }

    private getCart(cartId: string){
        return this.httpClient
        .get<any>(environment.apiUrl + '/Finances/Cart/' + cartId);
    }

    checkoutCart(){
      return this.httpClient
      .post<any>(environment.apiUrl + '/Finances/Cart/' + this.cartId  + '/checkout?walletId=' + this.walletService.currentWallet()?.id, {})
        .pipe(
          tap((response) => {
            localStorage.removeItem('cartId');
            this.walletService.getWallet(this.userService.currentUser()!.id);

            const addedCourseItems = this.courseService.courses().filter(course => this.cart()?.items.some(item => item.id === course.id))
            const addedCourses = addedCourseItems.map(courseItem => this.courseService.mapCourseItemToCourse(courseItem));
            this.purchasedItemsService.purchasedCourses.update( currentItems => [...currentItems, ...addedCourses])

            this.cart.set(null);
            this.cartId = null;
            this.cartItemsDisplay.set(null);
          })
        );
    }
}