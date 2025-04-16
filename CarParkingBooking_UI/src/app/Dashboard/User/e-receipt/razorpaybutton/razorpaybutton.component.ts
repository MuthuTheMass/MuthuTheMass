import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CustomerDetails } from '../../../../Service/Model/BookingDealerModal';
declare var Razorpay: any;

@Component({
  selector: 'app-razorpaybutton',
  standalone: true,
  templateUrl: './razorpaybutton.component.html',
  styleUrls: ['./razorpaybutton.component.css']
})
export class RazorpaybuttonComponent implements OnInit {
  @Input() paymentAmount: number = 0; // Amount in INR
  currency: string = 'INR';
  upiId: string = 'razorpay.me/@carparking1144';
  key:string = 'rzp_test_K5F8atqTrPzCOi';
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
        alert('Payment Successful! Payment ID: ' + response.razorpay_payment_id);
        this.paymentResult.emit({
          status: 'success',
          data: response
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
