import { Injectable } from '@angular/core';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyServiceService {
  busyServiceCount = 0;

  constructor(private ngxservice: NgxSpinnerService) { }
  busy(){
    this.busyServiceCount++;
    this.ngxservice.show(undefined, {
      type: 'timer',
      bdColor: 'rgba(255, 255, 255, 0.7)',
      color: '#333333'

    });
  }
  idle(){
    this.busyServiceCount--;
    if (this.busyServiceCount <= 0) {
      this.busyServiceCount = 0;
      this.ngxservice.hide();

     }
  }
}
