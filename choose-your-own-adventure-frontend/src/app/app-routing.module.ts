import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {QuizComponent} from './quiz/quiz/quiz.component';

const routes: Routes = [
  {path: '', component: QuizComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
