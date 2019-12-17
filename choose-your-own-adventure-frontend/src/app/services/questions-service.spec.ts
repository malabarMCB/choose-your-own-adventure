import { QuestionsService } from './questions-service';
import {TestBed} from '@angular/core/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('QuestionsService', () => {
  let service: QuestionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        QuestionsService,
        {provider: 'API_IRL', useValue: 'test url'}
      ],
      imports: [HttpClientTestingModule]
    });

    service = TestBed.get(QuestionsService);
  });

  it('should create an instance', () => {
    expect(service).toBeTruthy();
  });
});
