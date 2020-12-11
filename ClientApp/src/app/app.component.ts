import { AccountService } from './services/account.service';
import { AccountPowerToFly } from './modelsForService/AccountPowerToFly';
import { AddNewAccountDialogComponent } from './add-new-account-dialog/add-new-account-dialog.component';
import { Component, OnInit } from '@angular/core';
import { Account } from '../models/Account';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material';
import { config } from 'process';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'app';
  arrayOfAccount: Array<Account> = [];

    // PowerToFly
    coverLetterPowerToFly = '';
    jobLinksPowerToFly = '';
    checkBoxIgnore = false;

  constructor(public dialog: MatDialog, public accountService: AccountService, private _snackBar: MatSnackBar) {}


  ngOnInit(): void {
      this.accountService.getAccounts().subscribe(result => 
        { 
          result.map(x => this.arrayOfAccount.push(new Account(x['email'], x['password'], false)));
        });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddNewAccountDialogComponent, {
      width: '250px',
      data: {login: '', password: ''}
    });

    dialogRef.afterClosed().subscribe(result => {
      const accountForSave: AccountPowerToFly = { Email: result.login,  Password: result.password, Status: 1, Id: 1, SiteId: 1};
      this.accountService.addAccount(accountForSave).subscribe(x =>
        { this.arrayOfAccount.push({ Email: result.login, Password: result.password,  IsSelected: false})})
      // TODO: Need to call server and save acc
    });
  }

  chooseAccount(login: any): void {
    this.arrayOfAccount.forEach(x => x.IsSelected = false);
    this.arrayOfAccount.find(x => x.Email === login).IsSelected = true;
  }

  startParseByLinks() {
    let selectedAccount = this.arrayOfAccount.find(x => x.IsSelected === true);
    if (selectedAccount == null)
    {
      this._snackBar.open('First choose acoount', null, {
        duration: 2000,
      });

      return;
    }

    if (this.coverLetterPowerToFly === '' || this.jobLinksPowerToFly === '')
    {
      this._snackBar.open('Fill cover letter and job links fields', null, {
        duration: 2000,
      });
      return;
    }

    this.accountService.addParseByJobLinksSignal(this.coverLetterPowerToFly,selectedAccount.Email, this.jobLinksPowerToFly, this.checkBoxIgnore);
  }
}
