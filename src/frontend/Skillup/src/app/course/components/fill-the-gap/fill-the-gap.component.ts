import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import {
  CdkDrag,
  CdkDragDrop,
  CdkDropList,
  DragDropModule,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { FillTheGapCreatorComponent } from './fill-the-gap-creator/fill-the-gap-creator.component';
import { Sentence, Word } from '../../models/fill-the-gap/fill-the-gap.models';
import { ExerciseService } from '../../services/exercise.service';
import { ExerciseType } from '../../models/exercise.model';
import { single } from 'rxjs';
import { ButtonModule } from 'primeng/button';
@Component({
  selector: 'app-fill-the-gap',
  standalone: true,
  imports: [DragDropModule, CommonModule, ButtonModule],
  templateUrl: './fill-the-gap.component.html',
  styleUrls: ['./fill-the-gap.component.css'],
})
export class FillTheGapComponent implements OnInit {
  sentence = input.required<Sentence>();
  deletable = input<boolean>(false);
  onRemove = output<{event: Event, id: string}>();
  numberInList = input<number | null>(null);
  parts: { word: Word; container: Word[] }[] = [];

  ngOnInit(): void {
    this.parts = this.sentence().value
      .split(/({\d+})/)
      .filter((part) => part.trim() !== '')
      .map((part) => {
        const match = part.match(/^{(\d+)}$/);
        const index = match ? Number(match[1]) : -1;
        return {
          word: { value: part.replace(/[{}]/g, ''), index },
          container: [] as Word[],
        };
      });
  }

  

  dropInToSentence(event: CdkDragDrop<Word[]>) {
    const targetContainer = event.container.data;
    const sourceContainer = event.previousContainer.data;

    if (targetContainer.length === 0) {
      transferArrayItem(
        sourceContainer,
        targetContainer,
        event.previousIndex,
        0
      );
    }
  }

  dropInToContainer(event: CdkDragDrop<Word[]>) {
    transferArrayItem(
      event.previousContainer.data,
      event.container.data,
      event.previousIndex,
      event.currentIndex
    );
  }

  onDragEnd() {}

  todoPredicate = (
    drag: CdkDrag<{ word: Word; container: Word[] }>,
    drop: CdkDropList<{ word: Word; container: Word[] }[]>
  ): boolean => {
    return drop.data.length <= 0;
  };

  removeSentence(event: Event, senetenceId: string){
    this.onRemove.emit({event: event, id: senetenceId});
  }
}