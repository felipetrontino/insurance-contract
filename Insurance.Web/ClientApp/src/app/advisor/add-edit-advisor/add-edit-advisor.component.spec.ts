import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditAdvisorComponent } from './add-edit-advisor.component';

describe('AddEditAdvisorComponent', () => {
  let component: AddEditAdvisorComponent;
  let fixture: ComponentFixture<AddEditAdvisorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditAdvisorComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditAdvisorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
