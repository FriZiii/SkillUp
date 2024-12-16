import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-cart-empty',
  standalone: true,
  imports: [ButtonModule, RouterModule],
  templateUrl: './cart-empty.component.html',
  styleUrl: './cart-empty.component.css'
})
export class CartEmptyComponent {

}
