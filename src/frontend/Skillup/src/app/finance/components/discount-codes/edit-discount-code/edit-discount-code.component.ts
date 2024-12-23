import { Component, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DiscountCode, DiscountCodeType } from '../../../models/discountCodes.model';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DatePickerModule } from 'primeng/datepicker';
import { SelectModule } from 'primeng/select';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputTextModule } from 'primeng/inputtext';

function expirationAfterStart(control: AbstractControl) {
  const startDate = control.get('startDate')?.value;
  const expirationDate = control.get('expirationDate')?.value;

  if(expirationDate === null){return null};
  return expirationDate >= startDate ? null : { expirationInvalid: true };
}

@Component({
  selector: 'app-edit-discount-code',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule, ButtonModule, CheckboxModule, DatePickerModule, SelectModule, InputGroupModule, InputGroupAddonModule, InputTextModule],
  templateUrl: './edit-discount-code.component.html',
  styleUrl: './edit-discount-code.component.css'
})
export class EditDiscountCodeComponent implements OnChanges {
  discountCodeInput = input.required<DiscountCode | null>();
  onEdit = output<DiscountCode>();
  onClose = output();
  
  today: Date = new Date();

  discountCodeForm = new FormGroup({
    code: new FormControl('', {
      validators: [Validators.minLength(2), Validators.required],
    }),
    discountValue: new FormControl(0, {
      validators: [Validators.required, Validators.min(1), Validators.max(100)],
    }),
    startDate: new FormControl(this.today, {validators: [Validators.required] }),
    expirationDate: new FormControl<Date | null>({value: null, disabled: true}),
    appliesToEntireCart: new FormControl(true, {
      validators: [Validators.required],
    }),
    isActive: new FormControl(true, {
      validators: [Validators.required],
    }),
    isPublic: new FormControl(false, {
      validators: [Validators.required],
    })
  }, { validators: expirationAfterStart });
  

  expires = true;

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['discountCodeInput'] && this.discountCodeInput() !== null){
      const discountCode = this.discountCodeInput()!;
      console.log(this.discountCodeInput());
      this.discountCodeForm.patchValue({
        code: discountCode.code,
        discountValue: discountCode.discountValue,
        appliesToEntireCart: discountCode.appliesToEntireCart,
        isActive: discountCode.isActive,
        isPublic: discountCode.isPublic,
      });

    }
  }

  changeExpire(){
    const expirationDateControl = this.discountCodeForm.get('expirationDate');
    if(!this.expires){
      expirationDateControl?.enable();
      this.discountCodeForm.controls.expirationDate.setValue(this.today);
    }
    else{
      expirationDateControl?.disable();
      this.discountCodeForm.controls.expirationDate.setValue(null);
      
  }
    }
  
  
      //Select
      types = Object.entries(DiscountCodeType).map(([name, value]) => ({
        name,
        value
    }));
      apliesToEntireCart = [
        { name: 'Applies to entire cart', value: true },
        { name: 'Applies for certain items', value: false },
    ];

    getAddonSymbol(): string {
      return this.discountCodeInput()?.type === DiscountCodeType.Percentage ? '%' : '$';
    }

    
    close(){
      this.onClose.emit();
    }

    editDiscountCode(event: Event){
    this.onEdit.emit({
      id: this.discountCodeInput()!.id,
      type: this.discountCodeInput()?.type ?? DiscountCodeType.FixedAmount,
      startAt: this.discountCodeForm.controls.startDate.value!,
      expireAt: this.discountCodeForm.controls.expirationDate.value,
      code: this.discountCodeForm.controls.code.value!,
    discountValue: this.discountCodeForm.controls.discountValue.value!,
    appliesToEntireCart: this.discountCodeForm.controls.appliesToEntireCart.value!,
    isActive: this.discountCodeForm.controls.isActive.value!,
    isPublic: this.discountCodeForm.controls.isPublic.value!,
    discountedItems: this.discountCodeInput()?.discountedItems ?? null
    })
    }
  
}
