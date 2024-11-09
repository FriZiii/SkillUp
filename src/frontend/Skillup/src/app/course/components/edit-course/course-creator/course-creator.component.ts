import { ChangeDetectorRef, Component, computed, inject, input, OnInit, signal } from '@angular/core';
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
import {CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray, DragDropModule, CdkDragEnter, CdkDragExit} from '@angular/cdk/drag-drop';
import { SectionItemComponent } from "./section-item/section-item.component";
import { FloatLabelModule } from 'primeng/floatlabel';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationDialogHandlerService } from '../../../../core/services/confirmation-handler.service';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ElementListComponent } from "./element-list/element-list.component";

@Component({
  selector: 'app-course-creator',
  standalone: true,
  imports: [AccordionModule, ButtonModule, DialogModule, InputTextModule, ReactiveFormsModule, CardModule, FormsModule, HiddenFormWrapperComponent, AddNewElementComponent, DragDropModule, SectionItemComponent, FloatLabelModule, ConfirmDialogModule, ElementListComponent],
  templateUrl: './course-creator.component.html',
  styleUrl: './course-creator.component.css'
})
export class CourseCreatorComponent implements OnInit {
  //FromUrl
  courseId = input.required<string>();
  //Services
  courseContentService = inject(CourseContentService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);
  //Variables
  sections = computed(() => this.courseContentService.sections());
  changeDetectorRef = inject(ChangeDetectorRef);

  ngOnInit(): void {
   this.courseContentService.getSectionsByCourseId(this.courseId());
  }


  //New Section
  newSectionTitle = signal('');
  submitSection(event: Event){
    this.confirmationDialogService.confirmSave(event, () => {
      this.courseContentService.addSection(this.courseId(), this.newSectionTitle(), this.sections().length).subscribe({
        next: (res) => {
          console.log(res);
        }
      })
    })
  }

  dropElement(event: CdkDragDrop<Section>) {
    moveItemInArray(event.container.data.elements, event.previousIndex, event.currentIndex);
    
    const element = event.container.data.elements.find(e => e.index === event.previousIndex);
    const section = this.sections().find(s => s.elements.find(e => e.id === element!.id));
    this.courseContentService.updateElementIndex(section!.id, element!.id, event.currentIndex).subscribe();

  } 

    dropSection(event: CdkDragDrop<Section[]>) {
      moveItemInArray(this.sections(), event.previousIndex, event.currentIndex);
      console.log(event);
      const section = this.sections().find(s => s.index === event.previousIndex);
      this.courseContentService.updateSectionIndex(section!.id, event.currentIndex).subscribe();
    }

    sectionEdit = false;
    changeSectionEditMode(event: boolean){
      this.sectionEdit = event;
    }

    
    elementEdit = false;
    changeElementEditMode(event: boolean){
      this.elementEdit = event;
    }

    onListEntered(event: CdkDragEnter) {
      // Możesz dodać tutaj logikę lub po prostu wymusić detekcję zmian.
      this.changeDetectorRef.detectChanges();
    }
    
    onListExited(event: CdkDragExit) {
      this.changeDetectorRef.detectChanges();
    }
}
