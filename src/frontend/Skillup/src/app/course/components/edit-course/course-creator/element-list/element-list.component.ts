import { CdkDrag, CdkDragDrop, CdkDragPlaceholder, CdkDropList, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, computed, inject, input } from '@angular/core';
import { Section } from '../../../../models/course-content.model';
import { ElementItemEditComponent } from "../element-item/element-item-edit.component";
import { CourseContentService } from '../../../../services/course-content.service';
import { CourseListItem } from '../../../../models/course.model';

@Component({
  selector: 'app-element-list',
  standalone: true,
  imports: [CdkDropList, CdkDrag, CdkDragPlaceholder, ElementItemEditComponent],
  templateUrl: './element-list.component.html',
  styleUrl: './element-list.component.css'
})
export class ElementListComponent {
  courseId = input.required<string>();
  section = input.required<Section>();
  course = input.required<CourseListItem>();
  elements = computed(() => this.section().elements);
  passSection = computed(() => this.section());
  courseContentService = inject(CourseContentService);

  dropElement(event: CdkDragDrop<Section[]>) {
    moveItemInArray(this.elements(), event.previousIndex, event.currentIndex);
    const element = this.elements().find(e => e.index === event.previousIndex);
    this.courseContentService.updateElementIndex(this.section().id, element!.id, event.currentIndex).subscribe();
  }

  
  elementEdit = false;
  changeElementEditMode(event: boolean){
    this.elementEdit = event;
  }
}
