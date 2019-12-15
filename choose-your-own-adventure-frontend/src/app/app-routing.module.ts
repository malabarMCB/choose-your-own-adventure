import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {QuizComponent} from './quiz/quiz/quiz.component';
import {ResultComponent} from './result/result/result.component';

const routes: Routes = [
  {path: '', component: QuizComponent, pathMatch: 'full'},
  {path: 'result', component: ResultComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
