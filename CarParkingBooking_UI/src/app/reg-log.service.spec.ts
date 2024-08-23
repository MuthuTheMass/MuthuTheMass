import { TestBed } from '@angular/core/testing';

import { RegLogService } from './reg-log.service';

describe('RegLogService', () => {
  let service: RegLogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegLogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
