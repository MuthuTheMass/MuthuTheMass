import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MuthubookComponent } from './muthubook.component';

describe('MuthubookComponent', () => {
  let component: MuthubookComponent;
  let fixture: ComponentFixture<MuthubookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MuthubookComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MuthubookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
