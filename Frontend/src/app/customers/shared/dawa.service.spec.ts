import { TestBed, inject } from '@angular/core/testing';

import { DawaService } from './dawa.service';

describe('DawaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DawaService]
    });
  });

  it('should be created', inject([DawaService], (service: DawaService) => {
    expect(service).toBeTruthy();
  }));
});
