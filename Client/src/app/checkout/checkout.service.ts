import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethods';
import { IOrderToCreate } from '../shared/models/Order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
baseurl = environment.apiURL;
  constructor(private http: HttpClient) { }
  getDeliveryMethods(){
    return this.http.get(this.baseurl + 'order/deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) => {
        return dm.sort((a, b ) => b.price - a.price);
      })
    );
  }
  createOrder(order: IOrderToCreate)
  {
    return this.http.post(this.baseurl + 'order', order);
  }
}
