import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCarrierComponent } from './list-carrier.component';

describe('ListCarrierComponent', () => {
  let component: ListCarrierComponent;
  let fixture: ComponentFixture<ListCarrierComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListCarrierComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListCarrierComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
