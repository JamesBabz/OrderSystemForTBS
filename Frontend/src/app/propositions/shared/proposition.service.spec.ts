import { TestBed, inject } from '@angular/core/testing';

import { PropositionService } from './proposition.service';

describe('PropositionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PropositionService]
    });
  });

  it('should be created', inject([PropositionService], (service: PropositionService) => {
    expect(service).toBeTruthy();
  }));
});
