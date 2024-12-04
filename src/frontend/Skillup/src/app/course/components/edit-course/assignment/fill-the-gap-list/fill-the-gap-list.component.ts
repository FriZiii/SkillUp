import { Component, inject, input, OnInit } from '@angular/core';
import { FillTheGapCreatorComponent } from "../../../fill-the-gap/fill-the-gap-creator/fill-the-gap-creator.component";
import { ExerciseService } from '../../../../services/exercise.service';
import { ExerciseType } from '../../../../models/exercise.model';
import { Sentence } from '../../../../models/fill-the-gap/fill-the-gap.models';
import { FillTheGapComponent } from "../../../fill-the-gap/fill-the-gap.component";

@Component({
  selector: 'app-fill-the-gap-list',
  standalone: true,
  imports: [FillTheGapCreatorComponent, FillTheGapComponent],
  templateUrl: './fill-the-gap-list.component.html',
  styleUrl: './fill-the-gap-list.component.css'
})
export class FillTheGapListComponent implements OnInit {
  assignmentId = input.required<string>();
  sentences: Sentence[] = [];

    //Serivces
    exerciseService = inject(ExerciseService);
  
    ngOnInit(): void {
      this.exerciseService.getExercises(this.assignmentId(), ExerciseType.FillTheGap).subscribe(
        (res) => {
          this.sentences = res;
        }
      )
    }

    newSentendeAdded(event: Sentence){
      console.log(event);
      this.sentences.push(event);
    }
}
