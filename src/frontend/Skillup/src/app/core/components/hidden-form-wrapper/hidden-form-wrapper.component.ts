import { Component, input, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-hidden-form-wrapper',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './hidden-form-wrapper.component.html',
  styleUrl: './hidden-form-wrapper.component.css'
})
export class HiddenFormWrapperComponent {
  btnText = input.required<string>();
  submit = output<Event>();
  visible = false;
  changeVisibility(){
    if(this.visible === false)
      this.visible = true;
    else
    this.visible = false;
  }

  onSubmit(event: Event){
    console.log('submituje')
    this.submit.emit(event);
    this.changeVisibility();
  }
}
