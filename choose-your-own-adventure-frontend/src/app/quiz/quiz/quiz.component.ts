import {Component, OnDestroy, OnInit} from '@angular/core';
import {QuestionsService} from '../../services/questions-service';
import {Question} from '../../models/question';
import {AppState, isQuizFinished} from '../../store/app-store.reducer';
import {Store} from '@ngrx/store';
import * as Actions from '../../store/app-store.actions';
import {Router} from '@angular/router';
import {QuestionAnswer} from '../../models/question-answer.enum';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss']
})
export class QuizComponent implements OnInit, OnDestroy {

  currentQuestion: Question;
  isQuizFinishedSubscription: Subscription;

  constructor(private readonly questionService: QuestionsService,
              private readonly store: Store<AppState>,
              private readonly router: Router) { }

  ngOnInit() {
    this.isQuizFinishedSubscription = this.store.select(isQuizFinished).subscribe(isFinished => {
      if (isFinished) {
        this.router.navigate(['result']);
      }
    });

    this.setCurrentQuestion(1);
  }

  onQuestionAnswered(answer: QuestionAnswer) {
    this.store.dispatch(Actions.addAnsweredQuestion({question: this.currentQuestion}));

    const nextQuestionId = answer === QuestionAnswer.Positive
      ? this.currentQuestion.positiveAnswerQuestionId
      : this.currentQuestion.negativeAnswerQuestionId;

    this.setCurrentQuestion(nextQuestionId);
  }

  private setCurrentQuestion(id: number) {
    this.questionService.getQuestion(id).subscribe(question => {
      if (!question.negativeAnswerQuestionId && !question.positiveAnswerQuestionId) {
        this.store.dispatch(Actions.addAnsweredQuestion({question}));
        this.store.dispatch(Actions.setQuizStatus({isFinished: true}));
      }

      this.currentQuestion = question;
    });
  }

  ngOnDestroy(): void {
    this.isQuizFinishedSubscription.unsubscribe();
  }
}
