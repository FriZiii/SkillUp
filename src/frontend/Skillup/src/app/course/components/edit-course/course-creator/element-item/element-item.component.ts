import { Component, input, signal } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ElementType, Element } from '../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-element-item',
  standalone: true,
  imports: [CardModule, ButtonModule, FormsModule, InputTextModule],
  templateUrl: './element-item.component.html',
  styleUrl: './element-item.component.css'
})
export class ElementItemComponent {
  element = input.required<Element>();
  editVisible = false;
  elementTitle = signal('');

  definedIcon(type: ElementType) : string{
    switch (type){
      case ElementType.Article:
        return 'pi pi-book';
        case ElementType.Video:
        return 'pi pi-video';
        case ElementType.Exercise:
        return 'pi pi-objects-column';
    }
  }

  changeEditVisibility(){
    if(this.editVisible)
      this.editVisible=false;
    else
      this.editVisible=true;
  }
}
