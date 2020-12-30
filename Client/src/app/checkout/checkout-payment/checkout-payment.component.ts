import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/Order';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  constructor(
    private basketservice: BasketService,
    private checkoutservice: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  submitOrder()
  {
    const basket = this.basketservice.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutservice.createOrder(orderToCreate).subscribe((order: IOrder) => {
      this.toastr.success('Order created successfully');
      this.basketservice.deleteLocalBasket(basket.id);
      const navigationExtras: NavigationExtras = {state: order};
      this.router.navigate(['checkout/success'], navigationExtras);
    }, error => {
      this.toastr.error(error.message);
      console.log(error);
    });
  }
  getOrderToCreate(basket: IBasket) {
   return{
    basketId: basket.id,
    deliveryMethodId:  +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
    shiptoAddress: this.checkoutForm.get('addressForm').value
   };
  }
}
