export interface Question {
  id: number;
  text: string;
  positiveAnswerQuestionId?: number;
  negativeAnswerQuestionId?: number;
}
