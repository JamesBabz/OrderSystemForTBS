import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VisitCreateComponent } from './visit-create.component';

describe('VisitCreateComponent', () => {
  let component: VisitCreateComponent;
  let fixture: ComponentFixture<VisitCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VisitCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VisitCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
