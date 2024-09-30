import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DregComponent } from './dreg.component';

describe('DregComponent', () => {
  let component: DregComponent;
  let fixture: ComponentFixture<DregComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DregComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DregComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
