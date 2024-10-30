import { Component, Input, input } from '@angular/core';
import { VehicleModal } from '../../Service/Model/VehicleModal';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [],
  templateUrl: './card.component.html',
  styleUrl: './card.component.css'
})
export class CardComponent {
@Input("data") data:VehicleModal | undefined;

ngOnInit(): void {
}
}
