import { Component, OnInit } from '@angular/core';
declare var Razorpay: any;

@Component({
  selector: 'app-razorpaybutton',
  standalone: true,
  templateUrl: './razorpaybutton.component.html',
  styleUrls: ['./razorpaybutton.component.css']
})
export class RazorpaybuttonComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  paymentAmount: number = 100; // Amount in INR
  currency: string = 'INR';
  upiId: string = 'razorpay.me/@carparking1144';
  key:string = 'rzp_test_K5F8atqTrPzCOi';
  initiatePayment() {
    const options = {
      key: this.key, // Replace with your Razorpay Key ID
      amount: this.paymentAmount * 100, // Amount in paise (e.g., 100 INR = 10000 paise)
      currency: this.currency,
      name: 'Your Company Name',
      description: 'Payment for Order',
      image: 'https://your-company-logo-url.com/logo.png', // Replace with your logo URL
      handler: (response: any) => {
        console.log('Payment Successful:', response);
        alert('Payment Successful! Payment ID: ' + response.razorpay_payment_id);
        // You can send the payment response to your backend for verification
      },
      prefill: {
        name: 'Customer Name',
        email: 'customer@example.com',
        contact: '9999999999',
        method: 'upi', // Specify UPI as the payment method
        upi: {
          vpa: this.upiId // Specify the UPI ID
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
    rzp.open();
  }

}
