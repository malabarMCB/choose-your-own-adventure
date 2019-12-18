import { QuestionsService } from './questions-service';
import {TestBed} from '@angular/core/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {HttpClient} from '@angular/common/http';

describe('QuestionsService', () => {
  let service: QuestionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        [{provide: QuestionsService, deps: [HttpClient], useFactory: (http: HttpClient) => new QuestionsService('test.com', http)}],
      ],
      imports: [HttpClientTestingModule]
    });

    service = TestBed.get(QuestionsService);
  });

  it('should create an instance', () => {
    expect(service).toBeTruthy();
  });
});
