export interface Question {
  Id: number;
  Text: string;
  PositiveAnswerQuestionId?: number;
  NegativeAnswerQuestionId?: number;
}
