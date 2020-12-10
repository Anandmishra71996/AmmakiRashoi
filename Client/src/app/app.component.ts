import { Component, OnInit } from '@angular/core';
import { error } from 'console';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Amma Ki Rashoi';
  constructor(private basketservice: BasketService) {}
  ngOnInit(): void {
    const basketid = localStorage.getItem('basket_id');
    if (basketid) {
      this.basketservice.getbasket(basketid).subscribe(
        () => {
          console.log('Initialised Basket');
        },
        error => {
          console.log(error);
        }
      );
    }
  }
}
