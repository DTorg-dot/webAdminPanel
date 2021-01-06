import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Account } from 'src/models/Account';
import { AddNewAccountDialogComponent } from '../add-new-account-dialog/add-new-account-dialog.component';
import { AccountPowerToFly, AccountStatus } from '../modelsForService/accountPowerToFly';
import { PowerToFlyService } from '../services/power-to-fly.service';
import { Observable } from 'rxjs';
import { config } from 'process';

@Component({
  selector: 'app-power-to-fly-tab',
  templateUrl: './power-to-fly-tab.component.html',
  styleUrls: ['./power-to-fly-tab.component.css']
})
export class PowerToFlyTabComponent implements OnInit {

  arrayOfAccount: Array<Account> = [];

  coverLetterPowerToFly = '';
  jobLinksPowerToFly = '';
  checkBoxIgnore = false;
  donePowertofly: any;
  allJobsPowerToFly: any;
  pageCount: any;

  constructor(public dialog: MatDialog, public powertToFlyService: PowerToFlyService, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.powertToFlyService.getAccounts().subscribe(result => 
      { 
        result.map(x =>  {
          let status = this.getStatusEnumValue(x['status']);
          this.arrayOfAccount.push(new Account(x['email'], x['password'], false, status))
        });
      });
      this.powertToFlyService.getStatus().subscribe(result => 
        { 
          this.allJobsPowerToFly = result['allCount'];
          this.donePowertofly = result['doneCount'];
        });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddNewAccountDialogComponent, {
      width: '250px',
      data: {login: '', password: ''}
    });

    dialogRef.afterClosed().subscribe(result => {
      const accountForSave: AccountPowerToFly = { Email: result.login,  Password: result.password, Status: 1, Id: 1, SiteId: 1};
      this.powertToFlyService.addAccount(accountForSave).subscribe(x =>
        { 
          let status = this.getStatusEnumValue(1);
          this.arrayOfAccount.push({ Email: result.login, Password: result.password,  IsSelected: false, Status: status})
        })
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

    if (this.coverLetterPowerToFly === '' || this.jobLinksPowerToFly === '' || this.pageCount === '')
    {
      this._snackBar.open('Fill cover letter, page link, max page count fields', null, {
        duration: 2000,
      });
      return;
    }

    this.powertToFlyService.addParseByJobLinksSignal(this.coverLetterPowerToFly,selectedAccount.Email, this.jobLinksPowerToFly, this.checkBoxIgnore, this.pageCount).subscribe(_=> {
      this._snackBar.open('Bot signal created', null, {
        duration: 2000,
      });
    });
  }

  getStatusEnumValue(number: number) : AccountStatus
  {
    let stauts;
    switch (number) {
      case number: 1
      stauts = AccountStatus[number];
        break;
      case number: 2
      stauts = AccountStatus[number];
        break;
      case number: 3
      stauts = AccountStatus[number];
        break;
      default:
        break;
    }

    return stauts;
  }
}
