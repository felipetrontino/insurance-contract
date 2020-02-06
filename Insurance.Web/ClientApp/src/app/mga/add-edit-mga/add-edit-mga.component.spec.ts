import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditMgaComponent } from './add-edit-mga.component';

describe('AddEditMgaComponent', () => {
  let component: AddEditMgaComponent;
  let fixture: ComponentFixture<AddEditMgaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditMgaComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditMgaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
