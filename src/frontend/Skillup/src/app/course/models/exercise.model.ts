export interface Assignment{
    elementId: string;
    assetId: string;
    instruction: string;
    exerciseType: ExerciseType;
}

export enum ExerciseType{
    Quiz = "Quiz",
    QuestionAnswer = "QuestionAnswer",
    FillTheGap = "FillTheGap"
}

export interface QuestionAnswer{
    id: string;
    assignmentId: string;
    question: string;
    correctAnswer: string;
}

export interface Quiz{
    id: string;
    assignmentId: string;
    question: string;
    answers: QuizAnswer[];
}

export interface QuizAnswer{
    id: string;
    quizId: string;
    answer: string;
    isCorrect: boolean;
}