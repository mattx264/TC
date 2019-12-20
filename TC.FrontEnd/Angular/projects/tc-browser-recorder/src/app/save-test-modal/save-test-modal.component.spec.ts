import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaveTestModalComponent } from './save-test-modal.component';

describe('SaveTestModalComponent', () => {
  let component: SaveTestModalComponent;
  let fixture: ComponentFixture<SaveTestModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaveTestModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaveTestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
