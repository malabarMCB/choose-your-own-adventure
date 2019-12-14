import {Question} from '../models/question';
import {createReducer, on} from '@ngrx/store';
import * as Actions from './app-store.actions';

export interface AppState {
  answeredQuestions: Question[];
}

const initialState: AppState = {
  answeredQuestions: []
};

export const appReducer = createReducer(
  initialState,
  on(Actions.addAnsweredQuestion, (state, {question}) => ({...state, answeredQuestions: [...state.answeredQuestions, question]}))
);
