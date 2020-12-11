import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { AccountPowerToFly } from '../modelsForService/accountPowerToFly';
import { Account } from 'src/models/Account';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  constructor(private http: HttpClient) { }

  api_link: any = 'http://localhost:54379/api/';

  getAccounts(): Observable<any>
  {
     return this.http.get<any>(this.api_link + 'Account/Accounts');
  }

  addAccount(account: AccountPowerToFly)
  {
    return this.http.post(this.api_link + 'Account/AddAccount', account);
  }

  addParseByJobLinksSignal(coverLetter: string, email: string, jobLinks: string, ignoreAlreadySended: boolean)
  {
      this.http.post(this.api_link + 'JobPowerToFly/ParseByLinks', { coverLetter, AccountEmail: email, jobLinks, ignoreAlreadySended }).subscribe(x => console.log('success'));
  }
}