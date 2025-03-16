import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealeDetailsComponent } from './deale-details.component';

describe('DealeDetailsComponent', () => {
  let component: DealeDetailsComponent;
  let fixture: ComponentFixture<DealeDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DealeDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DealeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
