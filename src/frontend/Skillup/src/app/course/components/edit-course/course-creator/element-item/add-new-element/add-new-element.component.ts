import { Component, signal } from '@angular/core';
import { HiddenFormWrapperComponent } from "../../../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-add-new-element',
  standalone: true,
  imports: [HiddenFormWrapperComponent, FormsModule, CardModule, InputTextModule, InputTextareaModule, FloatLabelModule  ],
  templateUrl: './add-new-element.component.html',
  styleUrl: './add-new-element.component.css'
})
export class AddNewElementComponent {
  newElementTitle = signal('');
  newElementDescription = signal('');
  submitElement(){
    console.log(this.newElementTitle());
  }

}
