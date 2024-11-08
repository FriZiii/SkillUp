import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ElementType, Element, Section } from '../../../../models/course-content.model';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { NgClass } from '@angular/common';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';
import {CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray, DragDropModule} from '@angular/cdk/drag-drop';
import { CourseContentService } from '../../../../services/course-content-service';

@Component({
  selector: 'app-element-item',
  standalone: true,
  imports: [CardModule, ButtonModule, FormsModule, InputTextModule, NgClass, InputTextareaModule, FloatLabelModule, DragDropModule],
  templateUrl: './element-item.component.html',
  styleUrl: './element-item.component.css'
})
export class ElementItemComponent implements OnInit {
  section = input.required<Section>();
  element = input.required<Element>();
  editing = false;
  elementTitle = signal('');
  elementDescription = signal('');
  onEditChange = output<boolean>();

  //Services
  courseContentService = inject(CourseContentService);

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
    if(this.editing){
      this.editing=false;
      this.onEditChange.emit(false);
    }
    else {
      this.editing=true;
      this.onEditChange.emit(true);
    }
  }

  saveElement(){
    this.courseContentService.updateElement(this.section().id, this.element().id, this.elementTitle(), this.elementDescription()).subscribe();
    this.changeEditVisibility();
  }

  removeElement(){
    this.courseContentService.deleteElement(this.section().id, this.element().id).subscribe();
  }
}
