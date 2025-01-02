import { Component, input } from '@angular/core';
import { DiscountCode, DiscountCodeType } from '../../models/discountCodes.model';

@Component({
  selector: 'app-mini-code',
  standalone: true,
  imports: [],
  templateUrl: './mini-code.component.html',
  styleUrl: './mini-code.component.css'
})
export class MiniCodeComponent {
  code = input.required<DiscountCode>();

  getType(){
    if(this.code().type === DiscountCodeType.Percentage){
      return '%';
    }
    else{
      return '$';
    }
  }

  copyCode(){
    navigator.clipboard.writeText(this.code().code);
  }
}
