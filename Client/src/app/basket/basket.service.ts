import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {
  Basket,
  IBasket,
  IBasketItem,
  IBasketTotals,
} from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethods';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseurl = environment.apiURL;
  private basketsource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketsource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  baskettotal$ = this.basketTotalSource.asObservable();
  shipping = 0;
  constructor(private http: HttpClient) {}
  // tslint:disable-next-line: typedef
  getbasket(id: string) {
    return this.http.get(this.baseurl + 'basket?id=' + id).pipe(
      map((basket: IBasket) => {
        this.basketsource.next(basket);
        this.calculateTotals();
      })
    );
  }
  setBasket(basket: IBasket) {
    return this.http.post(this.baseurl + 'basket', basket).subscribe(
      (Response: IBasket) => {
        this.basketsource.next(Response);
        this.calculateTotals();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  setShippingPrice(deliveryMethod: IDeliveryMethod)
  {
    this.shipping = deliveryMethod.price;
    this.calculateTotals();
  }
  getCurrentBasketValue() {
    return this.basketsource.value;
  }
  addItemToBasket(item: IProduct, quantity = 1) {
    const itemtoadd: IBasketItem = this.mapProductItemtoBasketItem(
      item,
      quantity
    );
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemtoadd, quantity);
    this.setBasket(basket);
  }
  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundIndex = basket.items.findIndex(
      (x) => x.productId === item.productId
    );
    basket.items[foundIndex].quantity++;
    this.setBasket(basket);
  }
  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundIndex = basket.items.findIndex(
      (x) => x.productId === item.productId
    );
    if (basket.items[foundIndex].quantity > 1) {
      basket.items[foundIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeitemfrombasket(item);
    }
  }
  removeitemfrombasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    basket.items = basket.items.filter((x) => x.productId !== item.productId);
    if (basket.items.length > 0) {
      this.setBasket(basket);
    } else {
      this.removeBasket(basket);
    }
  }
  deleteLocalBasket(id: string)
  {
    this.basketsource.next(null),
        this.basketTotalSource.next(null),
        localStorage.removeItem('basket_id');
  }
  removeBasket(basket: IBasket) {
    this.http.delete(this.baseurl + 'basket?id=' + basket.id).subscribe(() => {
      this.basketsource.next(null),
        this.basketTotalSource.next(null),
        localStorage.removeItem('basket_id');
    },
      (error) => {
        console.log(error);
      });
  }

  private addOrUpdateItem(
    items: IBasketItem[],
    itemtoadd: IBasketItem,
    quantity: number
  ): IBasketItem[] {
    const index = items.findIndex((i) => i.productId === itemtoadd.productId);
    if (index === -1) {
      itemtoadd.quantity = quantity;
      items.push(itemtoadd);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }
  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }
  private mapProductItemtoBasketItem(
    item: IProduct,
    quantity: number
  ): IBasketItem {
    return {
      productId: item.id,
      productName: item.name,
      productPrice: item.price,
      quantity,
      type: item.productType,
      state: item.productState,
      pictureUrl: item.pictureUrl,
    };
  }
  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = this.shipping;
    const subtotal = basket.items.reduce(
      (a, b) => b.productPrice * b.quantity + a,
      0
    );
    const total = shipping + subtotal;
    this.basketTotalSource.next({ shipping, subtotal, total });
  }
}
