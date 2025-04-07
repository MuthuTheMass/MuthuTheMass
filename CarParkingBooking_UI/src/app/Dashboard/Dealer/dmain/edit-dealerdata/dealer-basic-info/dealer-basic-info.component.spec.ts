import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerBasicInfoComponent } from './dealer-basic-info.component';

describe('DealerBasicInfoComponent', () => {
  let component: DealerBasicInfoComponent;
  let fixture: ComponentFixture<DealerBasicInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DealerBasicInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DealerBasicInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
