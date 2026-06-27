import { TestBed } from '@angular/core/testing';

import { FollowUps } from './follow-ups';

describe('FollowUps', () => {
  let service: FollowUps;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FollowUps);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
