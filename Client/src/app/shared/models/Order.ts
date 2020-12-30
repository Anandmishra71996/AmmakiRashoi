import { IAddress } from './Address';

export interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shiptoAddress: IAddress;
}
export interface IOrder {
  buyeremail: string;
  orderDate: string;
  shipToAddress: IAddress;
  deliveryMethod: string;
  shippingPrice: number;
  orderItems: IOrderItem[];
  subtotal: number;
  total: number;
  status: string;
  paymentIntentId: string;
}

export interface IOrderItem {
  productId: number;
  productName: string;
  pictureUrl: string;
  quantity: number;
  price: number;
}
