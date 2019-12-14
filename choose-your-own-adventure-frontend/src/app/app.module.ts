import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { QuizComponent } from './quiz/quiz/quiz.component';
import { QuestionComponent } from './quiz/question/question.component';
import {HttpClientModule} from '@angular/common/http';
import {QuestionsService} from './services/questions-service';
import {AppStoreModule} from './store/app-store.module';

@NgModule({
  declarations: [
    AppComponent,
    QuizComponent,
    QuestionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AppStoreModule
  ],
  providers: [QuestionsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
