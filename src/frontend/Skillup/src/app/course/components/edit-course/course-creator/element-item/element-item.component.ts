import { Component, input, OnInit, signal } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ElementType, Element } from '../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { NgClass } from '@angular/common';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';
import {CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray, DragDropModule} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-element-item',
  standalone: true,
  imports: [CardModule, ButtonModule, FormsModule, InputTextModule, NgClass, InputTextareaModule, FloatLabelModule, DragDropModule],
  templateUrl: './element-item.component.html',
  styleUrl: './element-item.component.css'
})
export class ElementItemComponent implements OnInit {
  element = input.required<Element>();
  editing = false;
  elementTitle = signal('');
  elementDescription = signal('');

  ngOnInit(): void {
    this.elementTitle.set(this.element().title);
    this.elementDescription.set(this.element().description);
  }

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
    if(this.editing)
      this.editing=false;
    else
      this.editing=true;
  }

  saveElement(){
    console.log('saving element')
  }

  removeElement(){
    console.log('deleting element')
  }
}
