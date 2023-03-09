import { TestBed } from '@angular/core/testing';

import { LoaiTangVatService } from './loai-tang-vat.service';

describe('LoaiTangVatService', () => {
  let service: LoaiTangVatService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoaiTangVatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
