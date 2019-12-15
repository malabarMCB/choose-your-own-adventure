import {Component, OnInit} from '@angular/core';
import {QuestionsService} from '../../services/questions-service';
import {QuestionAnswer} from '../question/question.component';
import {Question} from '../../models/question';
import {AppState} from '../../store/app-store.reducer';
import {Store} from '@ngrx/store';
import * as Actions from '../../store/app-store.actions';
import {Router} from '@angular/router';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss']
})
export class QuizComponent implements OnInit {
  currentQuestion: Question;

  constructor(private readonly questionService: QuestionsService,
              private readonly store: Store<AppState>,
              private readonly router: Router) { }

  ngOnInit() {
    this.setCurrentQuestion(1);
  }

  onQuestionAnswered(answer: QuestionAnswer) {
    this.store.dispatch(Actions.addAnsweredQuestion({question: this.currentQuestion}));

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
      if (!question.negativeAnswerQuestionId && !question.positiveAnswerQuestionId) {
        console.log('it`s time to finish');
        this.router.navigate(['result']);
      }
      this.currentQuestion = question;
    });
  }
}
