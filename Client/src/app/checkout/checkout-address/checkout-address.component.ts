import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
@Input() checkoutForm;
  constructor(private accountservice: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
saveUserAddress()
{
  this.accountservice.updateUserAddress(this.checkoutForm.get('addressForm').value).subscribe(
    () => {
      this.toastr.success('Address Saved');
    }, error =>{
        this.toastr.error(error.message);
        console.log(error);
    });
}
}
