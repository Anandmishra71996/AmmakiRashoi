import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
basket$: Observable<IBasket>;
currentuser$: Observable<IUser>;
  constructor(private basketservice: BasketService ,private accountservice: AccountService) { }

  ngOnInit(): void {
    this.basket$ = this.basketservice.basket$;
    this.currentuser$ = this.accountservice.currentUser$;
  }
  logout()
  {
    this.accountservice.logout();
  }

}
