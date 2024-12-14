import { Component } from '@angular/core';
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
export class SolveQuestionComponent {
  questions: QuestionAnswer[] = [
    {
      id: '1',
      assignmentId: '1',
      question: 'What is the capital of France?',
      correctAnswer: 'Paris',
    },
    {
      id: '2',
      assignmentId: '1',
      question: 'What is 2 + 2?',
      correctAnswer: '4',
    },
    {
      id: '3',
      assignmentId: '1',
      question: 'What is the largest planet in the Solar System?',
      correctAnswer: 'Jupiter',
    },
    {
      id: '4',
      assignmentId: '1',
      question: 'What is the boiling point of water at sea level?',
      correctAnswer: '100°C',
    },
    {
      id: '5',
      assignmentId: '1',
      question: 'What is the chemical symbol for gold?',
      correctAnswer: 'Au',
    },
    {
      id: '6',
      assignmentId: '1',
      question: 'Which gas do plants primarily use for photosynthesis?',
      correctAnswer: 'Carbon Dioxide',
    },
  ];
  
  exerciseLength = this.questions.length;
  score = 0;
  answers: string[] = this.questions.map(q => '');
  wrongAnswers: string[] = [];
  submitted = false;

  submitAnswer(){
    for (let i = 0; i < this.questions.length; i++) {
      if(this.questions[i].correctAnswer.toLowerCase() !== this.answers[i].toLowerCase()){
        this.wrongAnswers[i] = this.questions[i].id;
      }
    }
    this.submitted = true;
  }

  tryAgain(){
    this.submitted = false;
    this.wrongAnswers = [];
  }
}
