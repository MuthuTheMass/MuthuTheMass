import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-error-message',
  standalone: true,
  imports: [],
  templateUrl: './error-message.component.html',
  styleUrl: './error-message.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ErrorMessageComponent implements OnInit {

  @Input() error?: any;

ngOnInit(): void {
 console.log(this.error);
}

}
