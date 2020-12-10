import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/Producttypes';
import { ShopParams } from '../shared/models/shopParams';
import { IState } from '../shared/models/State';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseurl = environment.apiURL;
  constructor(private http: HttpClient) {}
  // tslint:disable-next-line: typedef
  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.stateId !== 0) {
      params = params.append('stateId', shopParams.stateId.toString());
    }
    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.search)
    {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http
      .get<IPagination>(this.baseurl + 'products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
  // tslint:disable-next-line: typedef
  getProduct(id: number) {
    return this.http.get<IProduct>(this.baseurl + 'products/' + id);
  }
  // tslint:disable-next-line: typedef
  getStates() {
    return this.http.get<IState[]>(this.baseurl + 'products/states');
  }
  // tslint:disable-next-line: typedef
  getTypes() {
    return this.http.get<IType[]>(this.baseurl + 'products/types');
  }
}
