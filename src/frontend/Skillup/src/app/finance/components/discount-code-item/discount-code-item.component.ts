import { Component, input, OnInit, output } from '@angular/core';
import { DiscountCode, DiscountCodeType } from '../../models/discountCodes.model';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-discount-code-item',
  standalone: true,
  imports: [SelectButtonModule, FormsModule, InputTextModule, InputNumberModule, ButtonModule, CommonModule],
  templateUrl: './discount-code-item.component.html',
  styleUrl: './discount-code-item.component.css'
})
export class DiscountCodeItemComponent implements OnInit{
  discountCode = input.required<DiscountCode>();
  onEditCode = output<DiscountCode>();
  onDeleteCode = output<string>();

  editing = false;
  code = '';
  value = 1;
  entireCart = false;
  isActive = false;
  isPublic = false;

  getType(){
    if(this.discountCode().type === DiscountCodeType.FixedAmount){
      return '$';
    }
    else{
      return '%';
    }
  }

  stateOptions: any[] = [
    { label: 'No', value: false },
    { label: 'Yes', value: true },
  ];
  
  ngOnInit(): void {
    this.code = this.discountCode().code;
    this.value = this.discountCode().discountValue;
  this.entireCart = this.discountCode().appliesToEntireCart;
  this.isActive = this.discountCode().isActive;
  this.isPublic = this.discountCode().isPublic;
  }

  changeEditVisibility(){
    this.editing = !this.editing;
  }

  removeCode(event: Event){
    this.onDeleteCode.emit(this.discountCode().id);
  }

  editCode(){
    this.editing = !this.editing;
    this.onEditCode.emit({
      id: this.discountCode().id,
      type: this.discountCode().type,
      code: this.code,
    discountValue: this.value,
    appliesToEntireCart: this.entireCart,
    isActive: this.isActive,
    isPublic: this.isPublic,
    discountedItems: null
    })
  }

}
