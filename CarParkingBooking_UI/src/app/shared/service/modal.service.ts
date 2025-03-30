import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from '../modal/modal.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  constructor(private modalService: NgbModal) {}

  openConfirm(title: string, message: string): Promise<'confirmed' | 'cancelled'> {
    
    const modalRef = this.modalService.open(ModalComponent, { backdrop: 'static', keyboard: false });
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.message = message;

    return modalRef.result as Promise<'confirmed' | 'cancelled'>;
  }

}
