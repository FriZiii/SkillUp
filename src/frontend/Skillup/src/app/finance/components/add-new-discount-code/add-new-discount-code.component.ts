import { Component, inject, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DiscountCode, DiscountCodeType } from '../../models/discountCodes.model';
import { SelectModule } from 'primeng/select';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DiscountCodeService } from '../../services/discountCode.service';


@Component({
  selector: 'app-add-new-discount-code',
  standalone: true,
  imports: [InputTextModule, ReactiveFormsModule, CheckboxModule, InputNumberModule, SelectModule, CommonModule, ButtonModule],
  templateUrl: './add-new-discount-code.component.html',
  styleUrl: './add-new-discount-code.component.css'
})
export class AddNewDiscountCodeComponent {
  DiscountCodeType = DiscountCodeType;
  onAddCode = output<DiscountCode>();

  //Services
  disocuntCodeService = inject(DiscountCodeService);

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
    appliesToEntireCart: new FormControl(true, {
      validators: [Validators.required],
    }),
    isActive: new FormControl(true, {
      validators: [Validators.required],
    }),
    isPublic: new FormControl(false, {
      validators: [Validators.required],
    })
  });

    //Select
    types = Object.entries(DiscountCodeType).map(([name, value]) => ({
      name,
      value
  }));

  addNewDiscountCode(event: Event){
    this.disocuntCodeService.addDiscountCode(this.discountCodeForm.controls.codeType.value!, {
      code: this.discountCodeForm.controls.code.value!,
    discountValue: this.discountCodeForm.controls.discountValue.value!,
    appliesToEntireCart: this.discountCodeForm.controls.appliesToEntireCart.value!,
    isActive: this.discountCodeForm.controls.isActive.value!,
    isPublic: this.discountCodeForm.controls.isPublic.value!
    }).subscribe(
      (res) => this.onAddCode.emit(res)
    );
  }
}
