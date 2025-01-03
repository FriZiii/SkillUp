import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { ExerciseType } from "../models/exercise.model";
import { environment } from "../../../environments/environment";
import { Word } from "../models/fill-the-gap/fill-the-gap.models";

@Injectable({ providedIn: 'root' })
export class ExerciseService {
    private httpClient = inject(HttpClient);

    getExercises(assignmentId: string, exerciseType: ExerciseType) {
        return this.httpClient
      .get<any>(environment.apiUrl + '/Courses/Exercises/' + exerciseType + '/' + assignmentId);
      }

    addQuestionAnswer(assignmentId: string, question: string, answer: string){
        return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Exercises/Question/'+ assignmentId, {question: question, answer: answer});
    }

    addQuizQuestion(assignmentId: string, question: string, answers: string[], correct: boolean[]){
        return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Exercises/Quiz/'+ assignmentId, {question: question, answers: answers, correct: correct});
    }

    addFillTheGap(assignmentId: string, sentence: string, words: Word[]){
      return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Exercises/FillTheGap/'+ assignmentId, {sentence: sentence, words: words});
    }

    deleteExercise(exerciseType: ExerciseType, exerciseId: string){
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Exercises/'+  exerciseType + '/' + exerciseId);
    }
}