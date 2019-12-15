import {QuestionAnswer} from './question-answer.enum';

export interface QuestionTreeNode {
  id: number;
  text: string;
  type?: QuestionAnswer;
  children: QuestionTreeNode[];
}
