import {Component, OnInit} from '@angular/core';
import {QuestionsService} from '../../services/questions-service';
import {QuestionAnswer} from '../question/question.component';
import {Question} from '../../models/question';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss']
})
export class QuizComponent implements OnInit {
  currentQuestion: Question;

  constructor(private readonly questionService: QuestionsService) { }

  ngOnInit() {
    this.setCurrentQuestion(1);
  }

  onQuestionAnswered(answer: QuestionAnswer) {
    console.log('answer is ' + answer);
    const nextQuestionId = answer === QuestionAnswer.Positive
      ? this.currentQuestion.positiveAnswerQuestionId
      : this.currentQuestion.negativeAnswerQuestionId;

    if (!nextQuestionId) {
      console.log('game over');
    }

    this.setCurrentQuestion(nextQuestionId);
  }

  private setCurrentQuestion(id: number) {
    this.questionService.getQuestion(id).subscribe(question => {
      console.log(question);
      this.currentQuestion = question;
    });
  }
}
