import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IDeliveryMethod } from 'src/app/shared/models/deliveryMethods';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
@Input() checkoutForm;
deliveryMethods: IDeliveryMethod[];
  constructor(private checkoutservice: CheckoutService, private basketservice: BasketService) { }

  ngOnInit(): void {
    this.checkoutservice.getDeliveryMethods().subscribe((dm: IDeliveryMethod[]) => {
    this.deliveryMethods = dm;
    console.log(this.deliveryMethods);
    }, error => {
      console.log(error);
    });
  }
  setShippingPrice(deliverymethod: IDeliveryMethod)
  {
    this.basketservice.setShippingPrice(deliverymethod);
  }

}
