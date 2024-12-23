import { Component, computed, inject, input, OnInit, output, signal } from '@angular/core';
import { DiscountCode, DiscountCodeType } from '../../../models/discountCodes.model';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { CoursesService } from '../../../../course/services/course.service';
import { DiscountCodeService } from '../../../services/discountCode.service';
import { ConfirmationDialogHandlerService } from '../../../../core/services/confirmation-handler.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ItemType } from '../../../models/finance.model';
import { UserService } from '../../../../user/services/user.service';
import { ToastHandlerService } from '../../../../core/services/toast-handler.service';

@Component({
  selector: 'app-discount-code-item',
  standalone: true,
  imports: [SelectButtonModule, FormsModule, InputTextModule, InputNumberModule, ButtonModule, CommonModule, ConfirmDialogModule],
  templateUrl: './discount-code-item.component.html',
  styleUrl: './discount-code-item.component.css'
})
export class DiscountCodeItemComponent implements OnInit{
  discountCodeInput = input.required<DiscountCode>();
  discountCode = signal<DiscountCode | null>(null);
  onEditCode = output<DiscountCode>();
  onDeleteCode = output<string>();
  DiscountCodeType = DiscountCodeType;

  //Services
  courseService = inject(CoursesService);
  userService = inject(UserService);
  discountCodeService = inject(DiscountCodeService);
  confirmDialogService = inject(ConfirmationDialogHandlerService);
  toastService = inject(ToastHandlerService);

  courses = computed(() => {
    const discountedItems = this.discountCode()?.discountedItems || [];
    return discountedItems.length > 0 
        ? this.courseService.courses().filter(c => discountedItems.some(i => i.id === c.id)) 
        : [];
});

  editing = false;
  code = '';
  value = 1;
  entireCart = false;
  isActive = false;
  isPublic = false;

  getType(){
    if(this.discountCode()!.type === DiscountCodeType.FixedAmount){
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
    this.discountCode.set(this.discountCodeInput());
    this.code = this.discountCode()!.code;
    this.value = this.discountCode()!.discountValue;
  this.entireCart = this.discountCode()!.appliesToEntireCart;
  this.isActive = this.discountCode()!.isActive;
  this.isPublic = this.discountCode()!.isPublic;
  }

  changeEditVisibility(){
    this.editing = !this.editing;
  }

  removeCode(event: Event){
    this.onDeleteCode.emit(this.discountCode()!.id);
  }

/*   editCode(){
    this.editing = !this.editing;
    this.onEditCode.emit({
      id: this.discountCode()!.id,
      type: this.discountCode()!.type,
      code: this.code,
    discountValue: this.value,
    appliesToEntireCart: this.entireCart,
    isActive: this.isActive,
    isPublic: this.isPublic,
    discountedItems: null
    })
  } */

  removeItem(event: Event, id: string){
    this.confirmDialogService.confirmDelete(event, () =>{
      this.discountCodeService.toggleDiscountCodeItem(this.discountCode()!.id, id).subscribe(
        (res) => {
          this.discountCode.set(res);
        }
      )
    })
  }
  newItemId = '';
  addItem(){
    const coursesByAuthor = this.courseService.getCoursesByAuthor(this.userService.currentUser()!.id);
    if(coursesByAuthor.some(c => c.id === this.newItemId)){
      this.discountCodeService.toggleDiscountCodeItem(this.discountCode()!.id, this.newItemId).subscribe(
        (res) => {
          this.newItemId = '';
          this.discountCode.set(res);
        }
      )
    }
    else{
      this.toastService.showWarn('This is not your course');
    }
  }
}
