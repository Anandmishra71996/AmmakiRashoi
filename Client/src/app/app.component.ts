import { Component, OnInit } from '@angular/core';
import { error } from 'console';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Amma Ki Rashoi';
  constructor(
    private basketservice: BasketService,
    private accountservice: AccountService
  ) {}
  ngOnInit(): void {
    this.loadBasket();
    this.loadUser();
  }
  loadUser() {
    const token = localStorage.getItem('token');

    this.accountservice.loadCurrentUser(token).subscribe(
      () => {
        console.log('Current User Logged in');
      },
      (error) => {
        console.log(error);
      }
    );
  }
  loadBasket() {
    const basketid = localStorage.getItem('basket_id');
    if (basketid) {
      this.basketservice.getbasket(basketid).subscribe(
        () => {
          console.log('Initialised Basket');
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
