import { Component, EventEmitter, inject, Input, OnInit, Output, signal, TemplateRef, WritableSignal } from '@angular/core';
import { ModalDismissReasons, NgbActiveModal, NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {LoadingComponent} from "../loading/loading.component";

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
  standalone:true,
  imports: [NgbDatepickerModule, LoadingComponent]
})
export class ModalComponent {


  @Input() isLoading: boolean = true;
  @Input() title: string = 'Details';
  @Input() isOpen: boolean = false;
  @Output() closed = new EventEmitter<void>();

  closeModal() {
    this.closed.emit();
  }
}
