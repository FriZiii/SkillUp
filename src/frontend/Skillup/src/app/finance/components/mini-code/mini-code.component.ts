import { Component, input } from '@angular/core';
import { DiscountCode, DiscountCodeType } from '../../models/discountCodes.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mini-code',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mini-code.component.html',
  styleUrl: './mini-code.component.css'
})
export class MiniCodeComponent {
  code = input.required<DiscountCode>();
  copied = false;

  getType(){
    if(this.code().type === DiscountCodeType.Percentage){
      return '%';
    }
    else{
      return '$';
    }
  }

  copyCode(){
    this.copied = true;
    navigator.clipboard.writeText(this.code().code);

    setTimeout(() => {
      this.copied = false;
    }, 2000);
  }
}
