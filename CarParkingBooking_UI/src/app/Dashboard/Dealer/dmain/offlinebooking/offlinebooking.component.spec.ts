import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfflinebookingComponent } from './offlinebooking.component';

describe('OfflinebookingComponent', () => {
  let component: OfflinebookingComponent;
  let fixture: ComponentFixture<OfflinebookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfflinebookingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfflinebookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
