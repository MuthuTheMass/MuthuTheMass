import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { userSearchDealerDetailsResolver } from './user-search-dealer-details.resolver';

describe('userSearchDealerDetailsResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => userSearchDealerDetailsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
