import { Component, inject, input, OnInit } from '@angular/core';
import { FillTheGapCreatorComponent } from "../../../fill-the-gap/fill-the-gap-creator/fill-the-gap-creator.component";
import { ExerciseService } from '../../../../services/exercise.service';
import { ExerciseType } from '../../../../models/exercise.model';
import { Sentence } from '../../../../models/fill-the-gap/fill-the-gap.models';
import { FillTheGapComponent } from "../../../fill-the-gap/fill-the-gap.component";
import { ConfirmationDialogHandlerService } from '../../../../../core/services/confirmation-handler.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-fill-the-gap-list',
  standalone: true,
  imports: [FillTheGapCreatorComponent, FillTheGapComponent, ConfirmDialogModule],
  templateUrl: './fill-the-gap-list.component.html',
  styleUrl: './fill-the-gap-list.component.css'
})
export class FillTheGapListComponent implements OnInit {
  assignmentId = input.required<string>();
  sentences: Sentence[] = [];

    //Serivces
    exerciseService = inject(ExerciseService);
    confirmDialogService = inject(ConfirmationDialogHandlerService);
  
    ngOnInit(): void {
      this.exerciseService.getExercises(this.assignmentId(), ExerciseType.FillTheGap).subscribe(
        (res) => {
          this.sentences = res;
        }
      )
    }

    newSentendeAdded(event: Sentence){
      this.sentences.push(event);
    }

    removeSentence(data: {event: Event, id: string}){
      this.confirmDialogService.confirmDelete(data.event, () => {
      this.exerciseService.deleteExercise(ExerciseType.FillTheGap, data.id).subscribe(
        (res) => {
          this.sentences = this.sentences.filter(s => s.id !== data.id)
        }
      )
    })
    }
}
