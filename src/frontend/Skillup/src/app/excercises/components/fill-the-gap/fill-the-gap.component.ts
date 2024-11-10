import { Component } from '@angular/core';
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

@Component({
  selector: 'app-fill-the-gap',
  standalone: true,
  imports: [DragDropModule, CommonModule, FillTheGapCreatorComponent],
  templateUrl: './fill-the-gap.component.html',
  styleUrls: ['./fill-the-gap.component.css'],
})
export class FillTheGapComponent {
  sentence: Sentence = {
    id: '1',
    value: 'Bardzo lubię {0} kolorowe i te {1} piękne {2}',
    words: [
      { index: 0, value: 'kwiaty' },
      { index: 1, value: 'motyle' },
      { index: 2, value: 'latające' },
      { index: 3, value: 'kurwy' },
      { index: 4, value: 'dziwki' },
    ],
  };

  parts: { word: Word; container: Word[] }[];

  constructor() {
    this.parts = this.sentence.value
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
    console.log(event);
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
}
