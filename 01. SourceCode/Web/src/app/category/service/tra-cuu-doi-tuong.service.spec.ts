import { TestBed } from '@angular/core/testing';

import { TraCuuDoiTuongService } from './tra-cuu-doi-tuong.service';

describe('TraCuuDoiTuongService', () => {
  let service: TraCuuDoiTuongService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TraCuuDoiTuongService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
