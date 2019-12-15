import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Question} from '../models/question';

@Injectable()
export class QuestionsService {

  private baseUrl = 'https://76e870b5-dc16-4599-9ff9-ef511bc6697a.mock.pstmn.io'; /*todo make injectable*/

  constructor(private readonly http: HttpClient) {

  }

  getQuestion(id: number): Observable<Question> {
    return this.http.get<Question>(`${this.baseUrl}/questions/${id}`);
  }

  getAllQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.baseUrl}/questions`);
  }
}
