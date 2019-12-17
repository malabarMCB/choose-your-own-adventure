import {createAction, props} from '@ngrx/store';
import {Question} from '../models/question';

export const addAnsweredQuestion = createAction(
  '[App] Add answered question',
  props<{question: Question}>()
);

export const setQuizStatus = createAction(
  '[App] Set quiz status',
  props<{isFinished: boolean}>()
);

export const clearAnsweredQuestions = createAction(
  '[App] Clear answered questions'
);

export const setCurrentQuestion = createAction(
  '[App] Set current question',
  props<{question: Question}>()
);

export const clearCurrentQuestion = createAction(
  '[App] Clear current question'
);
