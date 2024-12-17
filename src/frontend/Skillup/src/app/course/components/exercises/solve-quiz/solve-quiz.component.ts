import { Component } from '@angular/core';
import { Quiz, QuizAnswer } from '../../../models/exercise.model';

@Component({
  selector: 'app-solve-quiz',
  standalone: true,
  imports: [],
  templateUrl: './solve-quiz.component.html',
  styleUrl: './solve-quiz.component.css'
})
export class SolveQuizComponent {
  quizes: Quiz[] = [
    {
      id: '1',
      assignmentId: '1',
      question: 'What is the capital of France?',
      answers: [
        { id: '1', quizId: '1', answer: 'Paris', isCorrect: true },
        { id: '2', quizId: '1', answer: 'Berlin', isCorrect: false },
        { id: '3', quizId: '1', answer: 'Madrid', isCorrect: false },
        { id: '4', quizId: '1', answer: 'Rome', isCorrect: false },
      ],
    },
    {
      id: '2',
      assignmentId: '1',
      question: 'What is 2 + 2?',
      answers: [
        { id: '1', quizId: '2', answer: '3', isCorrect: false },
        { id: '2', quizId: '2', answer: '4', isCorrect: true },
        { id: '3', quizId: '2', answer: '5', isCorrect: false },
        { id: '4', quizId: '2', answer: '6', isCorrect: false },
      ],
    },
    {
      id: '3',
      assignmentId: '1',
      question: 'What is the largest planet in the Solar System?',
      answers: [
        { id: '1', quizId: '3', answer: 'Earth', isCorrect: false },
        { id: '2', quizId: '3', answer: 'Mars', isCorrect: false },
        { id: '3', quizId: '3', answer: 'Jupiter', isCorrect: true },
        { id: '4', quizId: '3', answer: 'Saturn', isCorrect: false },
      ],
    },
    {
      id: '4',
      assignmentId: '1',
      question: 'What is the boiling point of water at sea level?',
      answers: [
        { id: '1', quizId: '4', answer: '90°C', isCorrect: false },
        { id: '2', quizId: '4', answer: '100°C', isCorrect: true },
        { id: '3', quizId: '4', answer: '110°C', isCorrect: false },
        { id: '4', quizId: '4', answer: '120°C', isCorrect: false },
      ],
    },
    {
      id: '5',
      assignmentId: '1',
      question: 'What is the chemical symbol for gold?',
      answers: [
        { id: '1', quizId: '5', answer: 'Au', isCorrect: true },
        { id: '2', quizId: '5', answer: 'Ag', isCorrect: false },
        { id: '3', quizId: '5', answer: 'Fe', isCorrect: false },
        { id: '4', quizId: '5', answer: 'Hg', isCorrect: false },
      ],
    },
    {
      id: '6',
      assignmentId: '1',
      question: 'Which gas do plants primarily use for photosynthesis?',
      answers: [
        { id: '1', quizId: '6', answer: 'Oxygen', isCorrect: false },
        { id: '2', quizId: '6', answer: 'Carbon Dioxide', isCorrect: true },
        { id: '3', quizId: '6', answer: 'Nitrogen', isCorrect: false },
        { id: '4', quizId: '6', answer: 'Hydrogen', isCorrect: false },
      ],
    },
  ];
  
  iterator = 0;
  currentQuestion = this.quizes[this.iterator];
  score = 0;
  quizLength = this.quizes.length;
  isComplete = false;

  onAnswer(answer: QuizAnswer){
    const correctAnswers = this.currentQuestion.answers.filter(a => a.isCorrect === true);
    if(correctAnswers.includes(answer)){
      this.score += 1;
    }
    
    this.iterator += 1;
    if(this.iterator < this.quizLength)
    {
      this.currentQuestion = this.quizes[this.iterator];
    }
    else{
      this.isComplete = true;
    }
  }
}
