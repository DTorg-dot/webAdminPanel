import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Account } from 'src/models/Account';
import { AddNewAccountDialogComponent } from '../add-new-account-dialog/add-new-account-dialog.component';
import { AccountPowerToFly, AccountStatus } from '../modelsForService/accountPowerToFly';
import { PowerToFlyService } from '../services/power-to-fly.service';
import { Observable } from 'rxjs';
import { config } from 'process';
import { FlexJobService } from '../services/flexjob.service';

@Component({
  selector: 'app-flex-job-tab',
  templateUrl: './flex-job-tab.component.html',
  styleUrls: ['./flex-job-tab.component.css']
})
export class FlexJobTabComponent implements OnInit {

  arrayOfAccount: Array<Account> = [];

  coverLetter = '';
  jobLinks = '';
  checkBoxIgnore = false;
  done: any;
  allJobs: any;

  constructor(public dialog: MatDialog, public flexJobService: FlexJobService, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.flexJobService.getAccounts().subscribe(result => 
      { 
        result.map(x =>  {
          let status = this.getStatusEnumValue(x['status']);
          this.arrayOfAccount.push(new Account(x['id'], x['email'], x['password'], false, status))
        });
      });
      this.flexJobService.getStatus().subscribe(result => 
        { 
          this.allJobs = result['allCount'];
          this.done = result['doneCount'];
        });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddNewAccountDialogComponent, {
      width: '250px',
      data: {login: '', password: ''}
    });

    dialogRef.afterClosed().subscribe(result => {
      const accountForSave: AccountPowerToFly = { Email: result.login,  Password: result.password, Status: 1, Id: 1, SiteId: 1};
      this.flexJobService.addAccount(accountForSave).subscribe(x =>
        { 
          this.flexJobService.getAccounts().subscribe(result => 
            { 
              result.map(x =>  {
                let status = this.getStatusEnumValue(x['status']);
                this.arrayOfAccount.push(new Account(x['id'], x['email'], x['password'], false, status))
              });
            });
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

    if (this.coverLetter === '' || this.jobLinks === '')
    {
      this._snackBar.open('Fill cover letter and job links fields', null, {
        duration: 2000,
      });
      return;
    }

    this.flexJobService.addParseByJobLinksSignal(this.coverLetter,selectedAccount.Email, this.jobLinks, this.checkBoxIgnore).subscribe(_=> {
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
