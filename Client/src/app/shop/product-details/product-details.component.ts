import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;
  constructor(
    private shopservice: ShopService,
    private bcservice: BreadcrumbService,
    private activeroute: ActivatedRoute,
    private basketservice: BasketService
  ) {
    this.bcservice.set('@productdetails', '');
  }

  ngOnInit(): void {
    this.loadProduct();
  }
  addIteminBasket() {
    this.basketservice.addItemToBasket(this.product, this.quantity);
  }
  incrementQuantity() {
    this.quantity++;
  }
  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
  // tslint:disable-next-line: typedef
  loadProduct() {
    this.shopservice
      .getProduct(+this.activeroute.snapshot.paramMap.get('id'))
      .subscribe(
        (product) => {
          this.product = product;
          this.bcservice.set('@productdetails', product.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
