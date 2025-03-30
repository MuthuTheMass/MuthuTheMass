import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import QrCodeScannerIcon from '@mui/icons-material/QrCodeScanner';

@Component({
  selector: 'app-dmain',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './dmain.component.html',
  styleUrl: './dmain.component.css'
})
export class DmainComponent {

}
