import { Component, inject, Input, OnInit, Output, signal, TemplateRef, WritableSignal } from '@angular/core';
import { ModalDismissReasons, NgbActiveModal, NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
  standalone:true,
  imports: [NgbDatepickerModule]
})
export class ModalComponent implements OnInit {
  @Input() content:WritableSignal<any> = signal({} as any);
  @Output() closeResult: WritableSignal<string> = signal('');

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  private getDismissReason(reason: any): string {
		switch (reason) {
			case ModalDismissReasons.ESC:
				return 'by pressing ESC';
			case ModalDismissReasons.BACKDROP_CLICK:
				return 'by clicking on a backdrop';
			default:
				return `with: ${reason}`;
		}
	}

}
