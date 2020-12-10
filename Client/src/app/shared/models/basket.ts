import { from } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';
export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export interface IBasketItem {
  productId: number;
  productName: string;
  productPrice: number;
  quantity: number;
  state: string;
  type: string;
  pictureUrl: string;
}
export class Basket implements IBasket {
  id = uuidv4();
  items: IBasketItem[] = [];
}
export interface IBasketTotals{
  shipping: number;
  subtotal: number;
  total: number;
}
