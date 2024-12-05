import { HttpClient, HttpParams } from "@angular/common/http";
import { computed, inject, Injectable, signal } from "@angular/core";
import { Cart, CartItemForDisplay } from "../models/cart.model";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs";
import { CoursesService } from "../../course/services/course.service";

@Injectable({ providedIn: 'root' })
export class CartService
{
    private httpClient = inject(HttpClient);
    private cartId = localStorage.getItem('cartId');
    public cart = signal<Cart | null>(null);
    public cartItemsDisplay = signal<CartItemForDisplay[] | null>(null);

  constructor(){
    if(this.cartId){
      this.getCart(this.cartId).subscribe((res) => 
        {
          this.cart.set(res);
          //this.cartItemsDisplay.set(this.getItemsForDisplay());
          console.log(this.cart());
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
        .delete(environment.apiUrl + '/Finances/Cart/' + this.cartId + '/Items/' + itemId)
        .pipe(
          tap((response) => {
            if(prevItems && prevItems.length > 1){
              this.cart.update((c) => {
                if(!c) return c;
                return {
                  ...c,
                  items: c.items.filter(i => i.id !== itemId)
                };
              });
            }
            else{
              this.cart.set(null);
              this.cartItemsDisplay.set(null);
              localStorage.removeItem('cartId');
              this.cartId = null;
            }
            
            
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

    private getCart(cartId: string){
        return this.httpClient
        .get<any>(environment.apiUrl + '/Finances/Cart/' + cartId)
        .pipe(
        );
    }

    courseService = inject(CoursesService);
   /*  getItemsForDisplay(){
      console.log(this.cart())
      const courses = computed(() => this.cart()?.items.flatMap((item) => this.courseService.getCourseById(item.id)));

      const cartItemsForDisplay = computed(() => {
        const courseList = courses();
      
        return this.cart()?.items?.map(cartItem => {
          const course = courseList?.find(c => c.id === cartItem.id);
          return {
            id: cartItem.id,
            originalItem: cartItem.originalItem,
            price: cartItem.price,
            title: course?.title || 'Unknown Title',
            authorName: course?.authorName || 'Unknown Author',
            thumbnailUrl: course?.thumbnailUrl || '',
          };
        }) || null;
      });
      
      this.cartItemsDisplay = computed(() => cartItemsForDisplay())
        
    } */
}