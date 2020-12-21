import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscriber, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { AccountPowerToFly } from '../modelsForService/accountPowerToFly';
import { Account } from 'src/models/Account';

@Injectable({
  providedIn: 'root',
})
export class PowerToFlyService {

  constructor(private http: HttpClient) { }

  api_link: any = 'http://localhost:54379/api/Powertofly/';

  getAccounts(): Observable<any>
  {
     return this.http.get<any>(this.api_link + 'Accounts');
  }

  getStatus(): Observable<any>
  {
     return this.http.get<any>(this.api_link + 'Status');
  }

  addAccount(account: AccountPowerToFly)
  {
    return this.http.post(this.api_link + 'AddAccount', account);
  }

  addParseByJobLinksSignal(coverLetter: string, email: string, jobLinks: string, ignoreAlreadySended: boolean): Observable<any>
  {
      return this.http.post(this.api_link + 'ParseByLinks', { coverLetter, AccountEmail: email, jobLinks, ignoreAlreadySended });
  }
}