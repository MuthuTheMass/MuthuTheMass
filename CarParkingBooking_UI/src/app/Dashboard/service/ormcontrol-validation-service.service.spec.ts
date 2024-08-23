import { TestBed } from '@angular/core/testing';

import { OrmcontrolValidationServiceService } from './ormcontrol-validation-service.service';

describe('OrmcontrolValidationServiceService', () => {
  let service: OrmcontrolValidationServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrmcontrolValidationServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
