import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Question} from '../models/question';
import {map} from 'rxjs/operators';

@Injectable()
export class QuestionsService {

  constructor(private readonly http: HttpClient) {

  }

  getQuestion(id: number): Observable<Question> {
    return this.http.get('assets/question.json').pipe(map(response => response as Question));
  }
}
