import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { NotificationType } from '../../Service/Enums/NotificationType';

@Component({
  selector: 'app-error-message',
  standalone: true,
  imports: [],
  templateUrl: './error-message.component.html',
  styleUrl: './error-message.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ErrorMessageComponent {
  @Input() error?: any;

  get errorMessage(): string {
    if (!this.error) return ''; // If there's no error, return an empty string (removes the div)

    const errorMessages: Record<string, string> = {
      required: 'This field is required',
      minlength: `Minimum length is ${this.error?.minlength?.requiredLength}`,
      maxlength: `Maximum length is ${this.error?.maxlength?.requiredLength}`,
      pattern: 'Invalid format',
      dateTime_Local: 'Invalid date format',
      VehicleCheckBox: 'Please select at least one vehicle',
    };

    const firstErrorKey = Object.keys(this.error)[0];
    return errorMessages[firstErrorKey] || 'Invalid field';
  }
}
