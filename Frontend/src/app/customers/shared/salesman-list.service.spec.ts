import { TestBed, inject } from '@angular/core/testing';

import { SalesmanListService } from './salesman-list.service';

describe('SalesmanListServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SalesmanListService]
    });
  });

  it('should be created', inject([SalesmanListService], (service: SalesmanListService) => {
    expect(service).toBeTruthy();
  }));
});
