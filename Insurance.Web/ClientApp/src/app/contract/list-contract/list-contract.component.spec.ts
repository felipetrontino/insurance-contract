import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListContractComponent } from './list-contract.component';

describe('ListContractComponent', () => {
  let component: ListContractComponent;
  let fixture: ComponentFixture<ListContractComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListContractComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListContractComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
