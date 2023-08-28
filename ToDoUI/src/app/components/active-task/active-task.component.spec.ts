import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveTaskComponent } from './active-task.component';

describe('ActiveTaskComponent', () => {
  let component: ActiveTaskComponent;
  let fixture: ComponentFixture<ActiveTaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ActiveTaskComponent]
    });
    fixture = TestBed.createComponent(ActiveTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
