import { Component, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-hidden-form-wrapper',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './hidden-form-wrapper.component.html',
  styleUrl: './hidden-form-wrapper.component.css'
})
export class HiddenFormWrapperComponent {
  submit = output();
  visible = false;
  changeVisibility(){
    if(this.visible === false)
      this.visible = true;
    else
    this.visible = false;
  }

  onSubmit(){
    console.log('submituje')
    this.submit.emit();
  }
}
