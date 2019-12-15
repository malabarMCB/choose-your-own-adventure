import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Question} from '../models/question';
import {QuestionTreeNode} from '../models/question-tree-node';

@Injectable()
export class QuestionsService {

  private baseUrl = 'http://demo0468172.mockable.io'; /*todo make injectable*/

  constructor(private readonly http: HttpClient) {

  }

  getQuestion(id: number): Observable<Question> {
    return this.http.get<Question>(`${this.baseUrl}/questions/${id}`);
  }

  getAllQuestions(): Observable<QuestionTreeNode> {
    return this.http.get<QuestionTreeNode>(`assets/question-tree.json`);
  }
}
