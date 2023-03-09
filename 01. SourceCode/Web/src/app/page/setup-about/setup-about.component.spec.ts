import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetupAboutComponent } from './setup-about.component';

describe('SetupAboutComponent', () => {
  let component: SetupAboutComponent;
  let fixture: ComponentFixture<SetupAboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetupAboutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SetupAboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
