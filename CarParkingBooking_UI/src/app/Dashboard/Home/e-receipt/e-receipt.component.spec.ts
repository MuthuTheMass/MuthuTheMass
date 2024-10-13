import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EReceiptComponent } from './e-receipt.component';

describe('EReceiptComponent', () => {
  let component: EReceiptComponent;
  let fixture: ComponentFixture<EReceiptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EReceiptComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EReceiptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
