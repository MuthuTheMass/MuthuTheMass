import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CustomerDetails } from '../../../../../Service/Model/BookingDealerModal';
import { environment } from '../../../../../../environments/environment';
declare var Razorpay: any;

@Component({
  selector: 'app-offline-payment',
  templateUrl: './offline-payment.component.html',
  styleUrls: ['./offline-payment.component.css']
})
export class OfflinePaymentComponent implements OnInit {

  @Input() paymentAmount: number = 0; // Amount in INR
  currency: string = 'INR';
  upiId: string = environment.razorUpiId;
  key:string = environment.razorKey;
  @Output() paymentResult = new EventEmitter<any>();
  @Input() customerDetail: CustomerDetails = {} as CustomerDetails;
  constructor() { }

  ngOnInit() {
  }


  initiatePayment() {
    const options = {
      key: this.key,
      amount: this.paymentAmount * 100,
      currency: this.currency,
      name: 'ParkZone',
      description: 'Payment for Order',
      image: 'assets/ParkZone.png',
      handler: (response: any) => {
        // ✅ Payment successful
        console.log('Payment Successful:', response);
        this.paymentResult.emit({
          razorpay_payment_id: response,
          status: 'success',
        });
      },
      prefill: {
        name: 'Customer Name',
        email: 'customer@example.com',
        contact: this.customerDetail.mobileNumber,
        method: 'upi',
        upi: {
          vpa: this.upiId
        }
      },
      notes: {
        address: 'Customer Address'
      },
      theme: {
        color: '#F37254'
      }
    };
  
    const rzp = new Razorpay(options);
  
    // ❌ Handle failure or cancellation
    rzp.on('payment.failed', (response: any) => {
      console.error('Payment Failed:', response.error);
      alert('Payment Failed: ' + response.error.description);
      this.paymentResult.emit({
        status: 'failure',
        data: response
      });
    });
  
    rzp.open();
  }
  
  

}
