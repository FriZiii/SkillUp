import { CommonModule } from '@angular/common';
import { Component, computed, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { InputTextModule } from 'primeng/inputtext';
import { PartType } from '../../../../models/fill-the-gap/creator.models';

@Component({
  selector: 'app-part',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ButtonModule,
    DragDropModule,
    InputTextModule,
  ],
  templateUrl: './part.component.html',
  styleUrl: './part.component.css',
})
export class PartComponent {
  type = input.required<PartType>();
  isDragged = input.required<boolean>();

  value = '';

  cancel = output();
  inputChange = output<string>();
  draggedChange = output<boolean>();

  click() {
    this.cancel.emit();
  }

  onInputChanged(event: string) {
    this.inputChange.emit(event);
  }

  onDragStart() {
    this.draggedChange.emit(true);
  }

  onDragEnd() {
    this.draggedChange.emit(false);
  }
}
