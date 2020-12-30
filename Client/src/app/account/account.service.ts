import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAddress } from '../shared/models/Address';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseurl = environment.apiURL;
  private currentusersource = new ReplaySubject<IUser>(null);
  currentUser$ = this.currentusersource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }
  loadCurrentUser(token: string)
  {
    if (token === null)
    {
      this.currentusersource.next(null);
      return of(null);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseurl + 'account', {headers} ).pipe(
      map((user: IUser) => {
        if (user)
        {
          localStorage.setItem('token', user.tokens);
          this.currentusersource.next(user);
        }
      })
    );
  }

  login(values: any)
  {
    return this.http.post(this.baseurl + 'account/login', values).pipe
      (map((user: IUser) => {
        if (user)
        {
          localStorage.setItem('token', user.tokens);
          this.currentusersource.next(user);
        }
      })
    );
  }
  register(values: any)
  {
    return this.http.post(this.baseurl + 'account/register', values).pipe(
      map((user: IUser) => {
        if (user)
        {
          localStorage.setItem('token', user.tokens);
          this.currentusersource.next(user);
        }

      })
    );
  }
  logout(): void
  {
    localStorage.removeItem('token');
    this.currentusersource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string)
  {
    return this.http.get(this.baseurl + 'account/emailexists?email=' + email);
  }
  getUserAddress(){
    return this.http.get(this.baseurl + 'account/address');
  }
  updateUserAddress(Address: IAddress)
  {
    return this.http.put(this.baseurl + 'account/address', Address);
  }
}
