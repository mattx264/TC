import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WebRtcComponent } from './web-rtc.component';

describe('WebRtcComponent', () => {
  let component: WebRtcComponent;
  let fixture: ComponentFixture<WebRtcComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WebRtcComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebRtcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
