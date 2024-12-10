import { Component, input, output } from '@angular/core';
import { CartItemForDisplay } from '../../models/cart.model';
import { RouterModule } from '@angular/router';
import { TruncatePipe } from "../../../utils/pipes/truncate.pipe";
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [RouterModule, TruncatePipe, ButtonModule],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  cartItem = input.required<CartItemForDisplay>(); 
  removeItem = output<string>();
  deletable = input<boolean>(false);

  onRemoveItem(event: Event, id: string){
    this.removeItem.emit(id);
  }
}
