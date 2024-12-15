import { Component, computed, input, signal } from '@angular/core';
import { Quiz, QuizAnswer } from '../../../models/exercise.model';

@Component({
  selector: 'app-solve-quiz',
  standalone: true,
  imports: [],
  templateUrl: './solve-quiz.component.html',
  styleUrl: './solve-quiz.component.css'
})
export class SolveQuizComponent {
  quizes = input.required<Quiz[]>();
  instruction = input<string>('Complete exercises');
  
  iterator = signal(0);
  currentQuestion = computed(() => this.quizes()[this.iterator()]);
  score = 0;
  quizLength = computed(() => this.quizes().length);
  isComplete = false;

  onAnswer(answer: QuizAnswer){
    const correctAnswers = this.currentQuestion().answers.filter(a => a.isCorrect === true);
    if(correctAnswers.includes(answer)){
      this.score += 1;
    }
    
    //this.iterator += 1;
    if(this.iterator() < this.quizLength() - 1)
    {
      this.iterator.set(this.iterator() + 1);
      //this.currentQuestion() = this.quizes()[this.iterator];
    }
    else{
      this.isComplete = true;
    }
  }
}
