import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MySoftwareComponent } from './my-software.component';

describe('MySoftwareComponent', () => {
  let component: MySoftwareComponent;
  let fixture: ComponentFixture<MySoftwareComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MySoftwareComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MySoftwareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
