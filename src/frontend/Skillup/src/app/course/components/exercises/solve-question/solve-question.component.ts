import { AfterContentInit, Component, computed, input, Signal } from '@angular/core';
import { QuestionAnswer } from '../../../models/exercise.model';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-solve-question',
  standalone: true,
  imports: [InputTextModule, FormsModule, ButtonModule],
  templateUrl: './solve-question.component.html',
  styleUrl: './solve-question.component.css'
})
export class SolveQuestionComponent implements AfterContentInit {
  
  questions = input.required<QuestionAnswer[]>();
  instruction = input<string>('Complete exercises');
  
  exerciseLength = computed(() => this.questions().length);
  score = 0;
  answers: string[] = [];
  wrongAnswers: string[] = [];
  submitted = false;

  ngAfterContentInit(): void {
    this.answers = this.questions().map(q => '');
  }

  submitAnswer(){
    for (let i = 0; i < this.exerciseLength(); i++) {
      if(this.questions()[i].correctAnswer.toLowerCase() !== this.answers[i].toLowerCase()){
        this.wrongAnswers[i] = this.questions()[i].id;
      }
    }
    this.submitted = true;
  }

  tryAgain(){
    this.submitted = false;
    this.wrongAnswers = [];
    this.answers = this.questions().map(q => '');
  }
}
