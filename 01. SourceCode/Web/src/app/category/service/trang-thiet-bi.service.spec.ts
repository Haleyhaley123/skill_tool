import { TestBed } from '@angular/core/testing';

import { TrangThietBiService } from './trang-thiet-bi.service';

describe('TrangThietBiService', () => {
  let service: TrangThietBiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrangThietBiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
