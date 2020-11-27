import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/Producttypes';
import { ShopParams } from '../shared/models/shopParams';
import { IState } from '../shared/models/State';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  products: IProduct[];
  states: IState[];
  types: IType[];
  shopParams = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDsc' },
  ];
  totalCount: number;

  constructor(private shopService: ShopService) {}

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.getProducts();
    this.getStates();
    this.getTypes();
  }
  // tslint:disable-next-line: typedef
  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(
      (response) => {
        this.products = response.data;
        this.shopParams.pageSize = response.pageSize;
        this.shopParams.pageNumber = response.pageIndex;
        this.totalCount = response.count;
      },
      (error) => {
        console.log(error);
      }
    );
  }
  // tslint:disable-next-line: typedef
  getStates() {
    this.shopService.getStates().subscribe(
      (response) => {
        this.states = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }
  // tslint:disable-next-line: typedef
  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }
  // tslint:disable-next-line: typedef
  onStateSelected(stateId: number) {
    this.shopParams.stateId = stateId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  // tslint:disable-next-line: typedef
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  // tslint:disable-next-line: typedef
  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }
  // tslint:disable-next-line: typedef
  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }
  // tslint:disable-next-line: typedef
  onsearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }
  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
