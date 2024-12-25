import { Component, inject, input, OnInit, signal } from '@angular/core';
import { AddNewDiscountCodeComponent } from "./add-new-discount-code/add-new-discount-code.component";
import { DiscountCodeService } from '../../services/discountCode.service';
import { DiscountCode, DiscountCodeType } from '../../models/discountCodes.model';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { EditDiscountCodeComponent } from "./edit-discount-code/edit-discount-code.component";

@Component({
  selector: 'app-discount-codes',
  standalone: true,
  imports: [AddNewDiscountCodeComponent, ButtonModule, DialogModule, TableModule, CommonModule, EditDiscountCodeComponent],
  templateUrl: './discount-codes.component.html',
  styleUrl: './discount-codes.component.css'
})
export class DiscountCodesComponent implements OnInit {
  //Services
  discountCodeService = inject(DiscountCodeService);

  products = [

    { code: 'A1', name: 'Product 1', category: 'Category 1', quantity: 10 },
    { code: 'B2', name: 'Product 2', category: 'Category 2', quantity: 5 }
  ]

  //Variables
  authorId = input<string>();
  discountCodes = signal<DiscountCode[]>([]);
  addNewDiscountCodeDialogVisible = false;
  editDiscountCodeDialogVisible = false;
  currentDiscountCode: DiscountCode | null = null;

  ngOnInit(): void {
    this.discountCodeService.getDiscountCodesByOwner(this.authorId()!).subscribe(
         (res) => {
           this.discountCodes.set(res);
         }
       )
 }

 onEditDiscountCode(code: DiscountCode){
  this.currentDiscountCode = code;
  this.editDiscountCodeDialogVisible = true;
 }

 editCode(event: DiscountCode){
  this.discountCodeService.editDiscountCode(event.id, {
    code: event.code,
    startAt: event.startAt,
    expireAt: event.expireAt,
    discountValue: event.discountValue,
    appliesToEntireCart: event.appliesToEntireCart,
    isActive: event.isActive,
    isPublic: event.isPublic
  }).subscribe(
    (res) => {
      this.discountCodes.update((prevCodes) => 
        prevCodes.map(code => code.id === res.id ? {
          ...code, 
          code: res.code, 
          discountValue: res.discountValue, 
          appliesToEntireCart: res.appliesToEntireCart,
        isActive: res.isActive,
      isPublic: res.isPublic, 
      startAt: res.startAt,
      expireAt: res.expireAt
    } : code));
    }
  );
}

toggleItem(res: DiscountCode){
  this.discountCodes.update((prevCodes) => 
    prevCodes.map(code => code.id === res.id ? {
      ...code, 
      discountedItems: res.discountedItems} : code));
}

deleteCode(codeId: string){
  this.discountCodeService.deleteDiscountCode(codeId).subscribe(
    (res) => this.discountCodes.set(this.discountCodes().filter(c => c.id !== codeId))
  );
}

addDiscountCode(code: DiscountCode){
  this.discountCodes.update((list) => [...list, code])
  this.addNewDiscountCodeDialogVisible = false;
}

getType(codeType: DiscountCodeType){
    if(codeType === DiscountCodeType.FixedAmount){
      return '$';
    }
    else{
      return '%';
    }
  }
}
