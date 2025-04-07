import { Component, signal } from '@angular/core';
import { DealerBasicInfoComponent } from './dealer-basic-info/dealer-basic-info.component';

@Component({
  selector: 'app-edit-dealerdata',
  standalone: true,
  imports: [],
  templateUrl: './edit-dealerdata.component.html',
  styleUrl: './edit-dealerdata.component.css',
})
export class EditDealerdataComponent {
  steps = ['Basic Info', 'Address', 'Upload Document', 'Confirmation'];
  currentStep = signal(0);

  next(): void {
    const step = this.currentStep();
    if (step < this.steps.length - 1) {
      this.currentStep.set(step + 1);
    }
  }

  prev(): void {
    const step = this.currentStep();
    if (step > 0) {
      this.currentStep.set(step - 1);
    }
  }

  goTo(index: number): void {
    if (index <= this.currentStep()) {
      this.currentStep.set(index);
    }
  }
}
