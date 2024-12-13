import { Component, inject, input, signal } from '@angular/core';
import { HiddenFormWrapperComponent } from "../../../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';
import { StepperModule } from 'primeng/stepper';
import { ButtonModule } from 'primeng/button';
import { SelectButtonModule } from 'primeng/selectbutton';
import { CourseContentService } from '../../../../../services/course-content.service';
import { AssetType } from '../../../../../models/course-content.model';
import { CommonModule } from '@angular/common';
import { ConfirmationDialogHandlerService } from '../../../../../../core/services/confirmation-handler.service';

@Component({
  selector: 'app-add-new-element',
  standalone: true,
  imports: [CommonModule, StepperModule, ButtonModule, SelectButtonModule, FormsModule, CardModule, InputTextModule, InputTextareaModule, FloatLabelModule  ],
  templateUrl: './add-new-element.component.html',
  styleUrl: './add-new-element.component.css'
})
export class AddNewElementComponent {
  sectionId = input.required<string>();
  newElementTitle = signal('');
  newElementDescription = signal('');
  newElementType = signal<AssetType | null>(null);
  newElementFree = signal<boolean>(false);

  AssetType = AssetType;
  freeOptions: any[] = [{ label: 'Yes', value: true },{ label: 'No', value: false }];

  //Services
  courseContentService = inject(CourseContentService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);


  chooseType(type: AssetType){
    this.newElementType.set(type);
    console.log(this.newElementType());
  }

  submitElement(event: Event){
    this.confirmationDialogService.confirmSave(event, () => {
    this.courseContentService.addElement(this.sectionId(), this.newElementType()!, this.newElementTitle(), this.newElementDescription(), this.newElementFree()).subscribe();
    })
    this.changeVisibility();
  }

  
  visible = false;
  changeVisibility(){
    if(this.visible === false)
      this.visible = true;
    else
    this.visible = false;
  }
}
