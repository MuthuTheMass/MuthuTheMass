import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerMainpageComponent } from './customer-mainpage.component';

describe('CustomerMainpageComponent', () => {
  let component: CustomerMainpageComponent;
  let fixture: ComponentFixture<CustomerMainpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerMainpageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerMainpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
