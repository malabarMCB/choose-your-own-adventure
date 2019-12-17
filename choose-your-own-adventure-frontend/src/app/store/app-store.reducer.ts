import {Question} from '../models/question';
import {createFeatureSelector, createReducer, createSelector, on} from '@ngrx/store';
import * as Actions from './app-store.actions';

export interface AppState {
  answeredQuestions: Question[];
  isQuizFinished: boolean;
  currentQuestion: Question;
}

const initialState: AppState = {
  answeredQuestions: [],
  isQuizFinished: false,
  currentQuestion: null
};

export const appReducer = createReducer(
  initialState,
  on(Actions.addAnsweredQuestion, (state, {question}) => ({...state, answeredQuestions: [...state.answeredQuestions, question]})),
  on(Actions.setQuizStatus, (state, {isFinished}) => ({...state, isQuizFinished : isFinished})),
  on(Actions.clearAnsweredQuestions, state => ({...state, answeredQuestions: []})),
  on(Actions.setCurrentQuestion, (state, {question}) => ({...state, currentQuestion: question})),
  on(Actions.clearCurrentQuestion, state => ({...state, currentQuestion: null}))
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

export const getCurrentQuestion =
  createSelector(
    getAppState,
    state => state.currentQuestion
  );
