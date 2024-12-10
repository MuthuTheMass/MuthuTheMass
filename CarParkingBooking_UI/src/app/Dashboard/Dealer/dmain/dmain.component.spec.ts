import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DmainComponent } from './dmain.component';

describe('DmainComponent', () => {
  let component: DmainComponent;
  let fixture: ComponentFixture<DmainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DmainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DmainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
