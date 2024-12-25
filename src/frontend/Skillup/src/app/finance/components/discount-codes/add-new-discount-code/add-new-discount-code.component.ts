import { Component, inject, output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DiscountCode, DiscountCodeType } from '../../../models/discountCodes.model';
import { SelectModule } from 'primeng/select';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DiscountCodeService } from '../../../services/discountCode.service';
import { DatePickerModule } from 'primeng/datepicker';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { catchError, throwError } from 'rxjs';

function expirationAfterStart(control: AbstractControl) {
  const startDate = control.get('startDate')?.value;
  const expirationDate = control.get('expirationDate')?.value;

  if(expirationDate === null){return null};
  return expirationDate >= startDate ? null : { expirationInvalid: true };
}

@Component({
  selector: 'app-add-new-discount-code',
  standalone: true,
  imports: [InputTextModule, ReactiveFormsModule, FormsModule, CheckboxModule, InputNumberModule, SelectModule, CommonModule, ButtonModule, DatePickerModule, InputGroupModule, InputGroupAddonModule],
  templateUrl: './add-new-discount-code.component.html',
  styleUrl: './add-new-discount-code.component.css'
})
export class AddNewDiscountCodeComponent {
  DiscountCodeType = DiscountCodeType;
  onAddCode = output<DiscountCode>();
  onClose = output();

  //Services
  disocuntCodeService = inject(DiscountCodeService);

  today = new Date();
  expires = true;

  

  //Form
  discountCodeForm = new FormGroup({
    codeType: new FormControl(this.DiscountCodeType.FixedAmount, {
      validators: [Validators.required],
    }),
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

    //Select
    types = Object.entries(DiscountCodeType).map(([name, value]) => ({
      name,
      value
  }));
    apliesToEntireCart = [
      { name: 'Applies to entire cart', value: true },
      { name: 'Applies for certain items', value: false },
  ];


  changeExpire(){
    const expirationDateControl = this.discountCodeForm.get('expirationDate');
    if(!this.expires){
      expirationDateControl?.enable();
      expirationDateControl?.setValue(this.today);
    }
    else{
      expirationDateControl?.disable();
    expirationDateControl?.setValue(null);
      
  }
    }

  addNewDiscountCode(event: Event){
    this.disocuntCodeService.addDiscountCode(this.discountCodeForm.controls.codeType.value!, {
      startAt: this.discountCodeForm.controls.startDate.value!,
      expireAt: this.discountCodeForm.controls.expirationDate.value!,
      code: this.discountCodeForm.controls.code.value!,
    discountValue: this.discountCodeForm.controls.discountValue.value!,
    appliesToEntireCart: this.discountCodeForm.controls.appliesToEntireCart.value!,
    isActive: this.discountCodeForm.controls.isActive.value!,
    isPublic: this.discountCodeForm.controls.isPublic.value!
    }).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    ).subscribe(
      (res) => {
        this.onAddCode.emit(res);
        this.discountCodeForm.reset();
      }
    );
  }

  close(){
    this.onClose.emit();
  }

  getAddonSymbol(): string {
    return this.discountCodeForm.controls.codeType.value === this.DiscountCodeType.Percentage ? '%' : '$';
  }


}
