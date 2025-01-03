import { Component, inject, input, OnInit, signal } from '@angular/core';
import { HiddenFormWrapperComponent } from "../../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ExerciseType, QuestionAnswer } from '../../../../models/exercise.model';
import { ExerciseService } from '../../../../services/exercise.service';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationDialogHandlerService } from '../../../../../core/services/confirmation-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-question-list',
  standalone: true,
  imports: [HiddenFormWrapperComponent, FormsModule, InputTextModule, FloatLabelModule, ButtonModule, ConfirmDialogModule ],
  templateUrl: './question-list.component.html',
  styleUrl: './question-list.component.css'
})
export class QuestionListComponent implements OnInit {
//Variables
  assignmentId = input.required<string>();
  newQuestion = signal('');
  newAnswer = signal('');
  questions = signal<QuestionAnswer[]>([]);

  //Serivces
  exerciseService = inject(ExerciseService);
  confirmDialogService = inject(ConfirmationDialogHandlerService);
  
  
  ngOnInit(): void {
    this.exerciseService.getExercises(this.assignmentId(), ExerciseType.QuestionAnswer).subscribe(
      (res) => {
        this.questions.set(res);
      }
    )
  }

  addQuestion(event: Event){
      this.exerciseService.addQuestionAnswer(this.assignmentId(), this.newQuestion(), this.newAnswer()).subscribe(
        (res)=>{
          this.questions.update((list) => [...list, res])
          this.newQuestion.set('');
          this.newAnswer.set('');
        }
      );
  }

  removeQuestion(event: Event, questionId: string){
    this.confirmDialogService.confirmDelete(event, () => {
      this.exerciseService.deleteExercise(ExerciseType.QuestionAnswer, questionId).subscribe(
        (res) => {
          this.questions.set(this.questions().filter(q => q.id !== questionId))
        }
      )
    })
  }
}
