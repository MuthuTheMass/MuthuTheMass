import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealeraccountComponent } from './dealeraccount.component';

describe('DealeraccountComponent', () => {
  let component: DealeraccountComponent;
  let fixture: ComponentFixture<DealeraccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DealeraccountComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DealeraccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
