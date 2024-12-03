import { Component, inject, input, OnInit, signal } from '@angular/core';
import { HiddenFormWrapperComponent } from "../../../../../core/components/hidden-form-wrapper/hidden-form-wrapper.component";
import { FormsModule } from '@angular/forms';
import { InputText, InputTextModule } from 'primeng/inputtext';
import { ExerciseType, Quiz } from '../../../../models/exercise.model';
import { ExerciseService } from '../../../../services/exercise.service';
import { CheckboxModule } from 'primeng/checkbox';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-quiz-list',
  standalone: true,
  imports: [HiddenFormWrapperComponent, FormsModule, InputTextModule, CheckboxModule, FloatLabelModule],
  templateUrl: './quiz-list.component.html',
  styleUrl: './quiz-list.component.css'
})
export class QuizListComponent implements OnInit {
  //Variables
    assignmentId = input.required<string>();
    newQuestion = signal('');
    answers = ['', '', '', ''];
    correct = [true, true, true, true];
  
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
      console.log(this.answers)
      console.log(this.correct)
      this.exerciseService.addQuizQuestion(this.assignmentId(), this.newQuestion(), this.answers, this.correct).subscribe(
        (res) => {
          this.quizes.update((list) => [...list, res])
          this.newQuestion.set('');
          this.answers = ['', '', '', ''];
          this.correct = [true, true, true, true];
        }
      )
    }
}
