import {Component, OnDestroy, OnInit} from '@angular/core';
import {QuestionsService} from '../../services/questions-service';
import {Question} from '../../models/question';
import {AppState, getCurrentQuestion, isQuizFinished} from '../../store/app-store.reducer';
import {Store} from '@ngrx/store';
import * as Actions from '../../store/app-store.actions';
import {Router} from '@angular/router';
import {QuestionAnswer} from '../../models/question-answer.enum';
import {of, Subscription} from 'rxjs';
import {first, mergeMap} from 'rxjs/operators';

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
              private readonly router: Router) {
  }

  ngOnInit() {
    this.isQuizFinishedSubscription = this.store.select(isQuizFinished).subscribe(isFinished => {
      if (isFinished) {
        this.router.navigate(['result']);
      }
    });

    this.store.select(getCurrentQuestion).pipe(first(), mergeMap(question => {
      return question === null ? this.questionService.getFirstQuestion() : of(question);
    })).subscribe(question => this.currentQuestion = question);

  }

  onQuestionAnswered(answer: QuestionAnswer) {
    this.store.dispatch(Actions.addAnsweredQuestion({question: this.currentQuestion}));

    const nextQuestionId = answer === QuestionAnswer.Positive
      ? this.currentQuestion.positiveAnswerQuestionId
      : this.currentQuestion.negativeAnswerQuestionId;

    this.questionService.getQuestion(nextQuestionId).subscribe(question => {
      if (!question.negativeAnswerQuestionId && !question.positiveAnswerQuestionId) {
        this.store.dispatch(Actions.addAnsweredQuestion({question}));
        this.store.dispatch(Actions.clearCurrentQuestion());
        this.store.dispatch(Actions.setQuizStatus({isFinished: true}));
      } else {
        this.currentQuestion = question;
        this.store.dispatch(Actions.setCurrentQuestion({question}));
      }
    });
  }

  ngOnDestroy(): void {
    this.isQuizFinishedSubscription.unsubscribe();
  }
}
