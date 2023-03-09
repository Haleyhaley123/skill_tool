import { TestBed } from '@angular/core/testing';

import { LoaiMaTuyService } from './loai-ma-tuy.service';

describe('LoaiMaTuyService', () => {
  let service: LoaiMaTuyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoaiMaTuyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
