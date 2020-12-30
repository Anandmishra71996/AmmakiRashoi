import { Component, OnInit } from '@angular/core';
import { error } from 'console';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem } from '../shared/models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
basket$: Observable<IBasket>;
  constructor(private basketservice: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketservice.basket$;
  }

removeBasketItem(item: IBasketItem)
{
  this.basketservice.removeitemfrombasket(item);
}
incrementItemQuantity(item: IBasketItem)
{
  this.basketservice.incrementItemQuantity(item);
}
decrementItemQuantity(item: IBasketItem)
{
  if (item.quantity > 1)
 {
   this.basketservice.decrementItemQuantity(item);
 }
}
}
