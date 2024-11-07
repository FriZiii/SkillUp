import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { AccordionModule } from 'primeng/accordion';
import { ElementType, Section } from '../../../models/course-content.model';
import { CourseContentService } from '../../../services/course-content-service';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { FormsModule } from '@angular/forms';
import { HiddenFormWrapperComponent } from '../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component';
import { ElementItemComponent } from "./element-item/element-item.component";
import { AddNewElementComponent } from "./element-item/add-new-element/add-new-element.component";
import {CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray, DragDropModule} from '@angular/cdk/drag-drop';
import { SectionItemComponent } from "./section-item/section-item.component";
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-course-creator',
  standalone: true,
  imports: [AccordionModule, ButtonModule, DialogModule, InputTextModule, ReactiveFormsModule, CardModule, FormsModule, HiddenFormWrapperComponent, ElementItemComponent, AddNewElementComponent, DragDropModule, SectionItemComponent, FloatLabelModule],
  templateUrl: './course-creator.component.html',
  styleUrl: './course-creator.component.css'
})
export class CourseCreatorComponent implements OnInit {
  //FromUrl
  courseId = input.required<string>();
  //Services
  courseContentService = inject(CourseContentService);
  //Variables
  sections = computed(() => this.courseContentService.sections());

  ngOnInit(): void {
   this.courseContentService.getSectionsByCourseId(this.courseId());
  }


  //New Section
  newSectionTitle = signal('');
  submitSection(){
    console.log(this.newSectionTitle());
    console.log(this.sections().length);
    this.courseContentService.addSection(this.courseId(), this.newSectionTitle(), this.sections().length + 1).subscribe({
      next: (res) => {
        console.log(res);
      }
    })
  }

  dropElement(event: CdkDragDrop<Section>) {
    moveItemInArray(event.container.data.elements, event.previousIndex, event.currentIndex);
    console.log(event.container.data.elements);
  } 

    dropSection(event: CdkDragDrop<Section[]>) {
      console.log(event);
      moveItemInArray(this.sections(), event.previousIndex, event.currentIndex);
      console.log(this.sections())
    }
}
