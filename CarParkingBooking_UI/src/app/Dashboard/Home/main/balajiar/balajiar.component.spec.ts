import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BalajiarComponent } from './balajiar.component';

describe('BalajiarComponent', () => {
  let component: BalajiarComponent;
  let fixture: ComponentFixture<BalajiarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BalajiarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BalajiarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
