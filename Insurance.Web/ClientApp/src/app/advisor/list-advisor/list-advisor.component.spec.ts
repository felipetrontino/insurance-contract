import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAdvisorComponent } from './list-advisor.component';

describe('ListAdvisorComponent', () => {
  let component: ListAdvisorComponent;
  let fixture: ComponentFixture<ListAdvisorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ListAdvisorComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAdvisorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
