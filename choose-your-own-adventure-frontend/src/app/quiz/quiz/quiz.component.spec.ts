import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {QuizComponent} from './quiz.component';
import {QuestionComponent} from '../question/question.component';
import {QuestionsService} from '../../services/questions-service';
import {provideMockStore} from '@ngrx/store/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {AppState} from '../../store/app-store.reducer';

describe('QuizComponent', () => {
  let component: QuizComponent;
  let fixture: ComponentFixture<QuizComponent>;

  beforeEach(async(() => {
    const questionsServiceSpy = jasmine.createSpyObj('QuestionsService',
      ['getQuestion', 'getAllQuestions', 'getFirstQuestion']);
    const initialState = {
      answeredQuestions: [],
      isQuizFinished: false,
      currentQuestion: null
    };

    TestBed.configureTestingModule({
      declarations: [QuizComponent, QuestionComponent],
      imports: [RouterTestingModule],
      providers: [
        {provide: QuestionsService, useValue: questionsServiceSpy},
        provideMockStore({state: initialState})
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
