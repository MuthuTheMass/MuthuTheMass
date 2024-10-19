import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleBackendComponent } from './sample-backend.component';

describe('SampleBackendComponent', () => {
  let component: SampleBackendComponent;
  let fixture: ComponentFixture<SampleBackendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SampleBackendComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SampleBackendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
