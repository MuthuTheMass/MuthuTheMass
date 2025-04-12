import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserVehicleComponent } from './user-vehicle.component';

describe('UserdataComponent', () => {
  let component: UserVehicleComponent;
  let fixture: ComponentFixture<UserVehicleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserVehicleComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UserVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
