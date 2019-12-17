import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { QuizComponent } from './quiz/quiz/quiz.component';
import { QuestionComponent } from './quiz/question/question.component';
import {HttpClientModule} from '@angular/common/http';
import {QuestionsService} from './services/questions-service';
import {AppStoreModule} from './store/app-store.module';
import { ResultComponent } from './result/result/result.component';
import { DecisionTreeComponent } from './result/decision-tree/decision-tree.component';
import {environment} from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    QuizComponent,
    QuestionComponent,
    ResultComponent,
    DecisionTreeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AppStoreModule
  ],
  providers: [
    QuestionsService,
    {provide: 'API_URL', useValue: environment.apiUrl}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
