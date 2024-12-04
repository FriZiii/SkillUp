import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import {
  CdkDragDrop,
  DragDropModule,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { PartComponent } from './part/part.component';
import { TableModule } from 'primeng/table';
import { Sentence, Word } from '../../../models/fill-the-gap/fill-the-gap.models';
import Part, {
  FakeWord,
  PartType,
} from '../../../models/fill-the-gap/creator.models';
import { ExerciseService } from '../../../services/exercise.service';
@Component({
  selector: 'app-fill-the-gap-creator',
  standalone: true,
  imports: [
    TableModule,
    InputTextModule,
    FormsModule,
    DragDropModule,
    PartComponent,
    ToggleButtonModule,
    ButtonModule,
    CommonModule,
  ],
  templateUrl: './fill-the-gap-creator.component.html',
  styleUrl: './fill-the-gap-creator.component.css',
})
export class FillTheGapCreatorComponent {
  assignmentId = input.required<string>();
  sentenceAdded = output<Sentence>();
  exerciseService = inject(ExerciseService);

  parts: Part[] = [];
  dragged = false;
  sentence = new Sentence();

  fakeWord = '';
  fakeWords: FakeWord[] = [];

  addSentence() {
    this.parts.push(new Part(PartType.Sentence));
  }

  addWord() {
    this.parts.push(new Part(PartType.Word));
  }

  addFakeWord() {
    this.fakeWords.push({
      value: this.fakeWord,
    });

    this.fakeWord = '';
  }

  drop(event: CdkDragDrop<Part[]>) {
    transferArrayItem(
      event.previousContainer.data,
      event.container.data,
      event.previousIndex,
      event.currentIndex
    );
  }

  onDraggedChange(isDragged: boolean) {
    this.dragged = isDragged;
  }

  onDelete(index: number) {
    this.parts.splice(index, 1);
  }

  onPartValueChanged(index: number, value: string) {
    this.parts[index].value = value;
  }

  onSave() {
    let sentenceValue = '';
    let wordIndex = 0;
    const words: Word[] = [];

    this.parts.forEach((obj) => {
      if (obj.type === 1) {
        sentenceValue += `{${wordIndex}} `;
        words.push({ index: wordIndex, value: obj.value });
        wordIndex++;
      } else {
        sentenceValue += obj.value + ' ';
      }
    });

    this.sentence.value = sentenceValue.trim();
    this.sentence.words = words;

    const startingIndex = this.sentence.words.length;

    const newWords = this.fakeWords.map((fakeWord, i) => ({
      index: startingIndex + i,
      value: fakeWord.value,
    }));

    this.sentence.words.push(...newWords);

    console.log(this.sentence);
    this.exerciseService.addFillTheGap(this.assignmentId(), this.sentence.value, this.sentence.words).subscribe(
      (res) => {
        this.sentenceAdded.emit(res
        );
      }
    );
  }
}
