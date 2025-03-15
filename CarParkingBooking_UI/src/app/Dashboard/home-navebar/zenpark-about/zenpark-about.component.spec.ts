import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZenparkAboutComponent } from './zenpark-about.component';

describe('ZenparkAboutComponent', () => {
  let component: ZenparkAboutComponent;
  let fixture: ComponentFixture<ZenparkAboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ZenparkAboutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ZenparkAboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
