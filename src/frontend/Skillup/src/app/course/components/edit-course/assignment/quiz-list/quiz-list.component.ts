import { Component, inject, input, OnInit, signal } from '@angular/core';
import { HiddenFormWrapperComponent } from "../../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { FormsModule } from '@angular/forms';
import { InputText, InputTextModule } from 'primeng/inputtext';
import { ExerciseType, Quiz } from '../../../../models/exercise.model';
import { ExerciseService } from '../../../../services/exercise.service';
import { CheckboxModule } from 'primeng/checkbox';

@Component({
  selector: 'app-quiz-list',
  standalone: true,
  imports: [HiddenFormWrapperComponent, FormsModule, InputTextModule, CheckboxModule],
  templateUrl: './quiz-list.component.html',
  styleUrl: './quiz-list.component.css'
})
export class QuizListComponent implements OnInit {
  //Variables
    assignmentId = input.required<string>();
    newQuestion = signal('');
    newAnswer = signal('');
    newAnswerCorrect = signal<boolean>(true);
    quizes = signal<Quiz[]>([]);

    //Services
    exerciseService = inject(ExerciseService);
    
    ngOnInit(): void {
      this.exerciseService.getExercises(this.assignmentId(), ExerciseType.Quiz).subscribe(
        (res) => {
          console.log(res);
          this.quizes.set(res);
        }
      )
    }

    addQuiz(event: Event){
      this.exerciseService.addQuizQuestion(this.assignmentId(), this.newQuestion()).subscribe(
        (res) => {
          this.quizes.update((list) => [res, ...list])
        }
      )
    }

    addAnswer(event: Event, quizId: string){
      console.log(quizId);
      this.exerciseService.addQuizAnswer(quizId, this.newAnswer(), this.newAnswerCorrect()).subscribe(
        (res) => {
          const updatedQuizes = this.quizes().map(quiz => {
            if(quiz.id === quizId){
              return {
                ...quiz,
                answers: [
                  ...quiz.answers, res
                ]
              }
            }
            return quiz;
          });
          this.quizes.set(updatedQuizes);
        }
      )
    }
}
