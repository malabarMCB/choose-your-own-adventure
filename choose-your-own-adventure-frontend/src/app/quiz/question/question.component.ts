import {Component, EventEmitter, Input, Output} from '@angular/core';
import {QuestionAnswer} from '../../models/question-answer.enum';

@Component({
  selector: 'app-quiz-item',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent {
  @Input() text: string;

  @Output() questionAnswered = new EventEmitter<QuestionAnswer>();

  answerQuestion(answer: QuestionAnswer): boolean {
    this.questionAnswered.emit(answer);
    return false;
  }

}
