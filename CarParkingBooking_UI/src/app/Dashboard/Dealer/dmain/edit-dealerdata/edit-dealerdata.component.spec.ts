import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDealerdataComponent } from './edit-dealerdata.component';

describe('EditDealerdataComponent', () => {
  let component: EditDealerdataComponent;
  let fixture: ComponentFixture<EditDealerdataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditDealerdataComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditDealerdataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
