import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {QuestionsService} from '../../services/questions-service';
import {QuestionTreeNode} from '../../models/question-tree-node';
import {DecisionTreeComponent} from '../decision-tree/decision-tree.component';
import {Store} from '@ngrx/store';
import {AppState, getAnsweredQuestionsIds, isQuizFinished} from '../../store/app-store.reducer';
import {first} from 'rxjs/operators';
import * as Actions from '../../store/app-store.actions';
import {Router} from '@angular/router';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit, OnDestroy {
  @ViewChild(DecisionTreeComponent, {static: false}) decisionTree: DecisionTreeComponent;

  questionsTree: QuestionTreeNode;
  answeredQuestionsIds: number[];
  isQuizFinishedSubscription: Subscription;

  constructor(private readonly questionsService: QuestionsService,
              private readonly store: Store<AppState>,
              private readonly router: Router) { }

  ngOnInit() {
    this.questionsService.getAllQuestions().subscribe(questions => this.questionsTree = questions);
    this.store.select(getAnsweredQuestionsIds).pipe(first()).subscribe(ids => this.answeredQuestionsIds = ids);

    this.isQuizFinishedSubscription = this.store.select(isQuizFinished).subscribe(isFinished => {
      if (!isFinished) {
        this.router.navigate(['']);
      }
    });
  }

  highlightAnsweredQuestions(button: HTMLButtonElement): boolean {
    button.disabled = true;
    this.decisionTree.highlightNodes(this.answeredQuestionsIds);
    return false;
  }

  playAgain(): boolean {
    this.store.dispatch(Actions.clearAnsweredQuestions());
    this.store.dispatch(Actions.setQuizStatus({isFinished: false}));
    return false;
  }

  ngOnDestroy(): void {
    this.isQuizFinishedSubscription.unsubscribe();
  }
}
