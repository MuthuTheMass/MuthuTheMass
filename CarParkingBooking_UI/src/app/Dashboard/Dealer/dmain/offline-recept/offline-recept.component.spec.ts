import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfflineReceptComponent } from './offline-recept.component';

describe('OfflineReceptComponent', () => {
  let component: OfflineReceptComponent;
  let fixture: ComponentFixture<OfflineReceptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfflineReceptComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfflineReceptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
