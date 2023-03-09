import { TestBed } from '@angular/core/testing';

import { ThamQuyenXpvphcService } from './tham-quyen-xpvphc.service';

describe('ThamQuyenXpvphcService', () => {
  let service: ThamQuyenXpvphcService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThamQuyenXpvphcService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
