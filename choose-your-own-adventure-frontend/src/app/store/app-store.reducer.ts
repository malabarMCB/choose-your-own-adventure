import {Question} from '../models/question';
import {createFeatureSelector, createReducer, createSelector, on} from '@ngrx/store';
import * as Actions from './app-store.actions';

export interface AppState {
  answeredQuestions: Question[];
  isQuizFinished: boolean;
}

const initialState: AppState = {
  answeredQuestions: [],
  isQuizFinished: false
};

export const appReducer = createReducer(
  initialState,
  on(Actions.addAnsweredQuestion, (state, {question}) => ({...state, answeredQuestions: [...state.answeredQuestions, question]})),
  on(Actions.setQuizStatus, (state, {isFinished}) => ({...state, isQuizFinished : isFinished})),
  on(Actions.clearAnsweredQuestions, (state) => ({...state, answeredQuestions: []}))
);

export const getAppState = createFeatureSelector<AppState>('state');

export const getAnsweredQuestionsIds =
  createSelector(
    getAppState,
    state => state.answeredQuestions.map(question => question.id)
  );

export const isQuizFinished =
  createSelector(
    getAppState,
    state => state.isQuizFinished
  );
