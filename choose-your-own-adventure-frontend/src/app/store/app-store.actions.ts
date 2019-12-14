import {createAction, props} from '@ngrx/store';
import {Question} from '../models/question';

export const addAnsweredQuestion = createAction(
  '[App] Add answered question',
  props<{question: Question}>()
);
