import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Question} from '../models/question';
import {QuestionTreeNode} from '../models/question-tree-node';

@Injectable()
export class QuestionsService {

  constructor(@Inject('API_URL') private readonly  baseUrl, private readonly http: HttpClient) {

  }

  getQuestion(id: number): Observable<Question> {
    return this.http.get<Question>(`${this.baseUrl}/questions/${id}`);
  }

  getAllQuestions(): Observable<QuestionTreeNode> {
    return this.http.get<QuestionTreeNode>(`${this.baseUrl}/questions/tree`);
  }

  getFirstQuestion(): Observable<Question> {
    return this.http.get<Question>(`${this.baseUrl}/questions/first`);
  }
}
