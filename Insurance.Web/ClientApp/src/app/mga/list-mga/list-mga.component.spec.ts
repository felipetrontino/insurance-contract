import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMgaComponent } from './list-mga.component';

describe('ListMgaComponent', () => {
  let component: ListMgaComponent;
  let fixture: ComponentFixture<ListMgaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ListMgaComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListMgaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
