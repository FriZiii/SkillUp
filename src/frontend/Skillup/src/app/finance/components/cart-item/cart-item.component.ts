import { Component, input, output } from '@angular/core';
import { CartItemForDisplay } from '../../models/cart.model';
import { RouterModule } from '@angular/router';
import { TruncatePipe } from "../../../utils/pipes/truncate.pipe";
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [RouterModule, TruncatePipe, ButtonModule, CommonModule, RatingModule, FormsModule],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  cartItem = input.required<CartItemForDisplay>(); 
  removeItem = output<string>();
  deletable = input<boolean>(false);
  styleForSummary = input<boolean>(false);
  styleForOrder = input<boolean>(false);

  onRemoveItem(event: Event, id: string){
    this.removeItem.emit(id);
  }
}
