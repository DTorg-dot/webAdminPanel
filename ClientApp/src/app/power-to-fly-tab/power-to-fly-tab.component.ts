import { BotSignalStatus } from './../modelsForService/AccountPowerToFly';
import { map } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Account } from 'src/models/Account';
import { AddNewAccountDialogComponent } from '../add-new-account-dialog/add-new-account-dialog.component';
import { AccountPowerToFly, AccountStatus, BotSignal } from '../modelsForService/accountPowerToFly';
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
  arrayOfBotSignals: Array<BotSignal> = [];

  coverLetterPowerToFly = '';
  jobLinksPowerToFly = '';
  checkBoxIgnore = false;
  donePowertofly: any;
  allJobsPowerToFly: any;
  pageCount: any;
  log: string = '';

  // BotSignal

  selectedBotSignal: BotSignal;
    
  constructor(public dialog: MatDialog, public powertToFlyService: PowerToFlyService, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.powertToFlyService.getAccounts().subscribe(result => 
      { 
        result.map(x =>  {
          let status = this.getStatusEnumValue(x['status']);
          this.arrayOfAccount.push(new Account(x['id'],x['email'], x['password'], false, status))
        });
      });
      this.powertToFlyService.getStatus().subscribe(result => 
        { 
          this.allJobsPowerToFly = result['allCount'];
          this.donePowertofly = result['doneCount'];
        });

        setInterval( () => {
          if (this.log.length > 5000)
          {
            this.log = '';
          }
          this.powertToFlyService.getLog().subscribe(result => 
            { 
              result.map(x => this.log += '\n' + x);
            });
       }, 2000);
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
          this.powertToFlyService.getAccounts().subscribe(result => 
            { 
              this.arrayOfAccount = [];
              result.map(x =>  {
                let status = this.getStatusEnumValue(x['status']);
                this.arrayOfAccount.push(new Account(x['Id'],x['email'], x['password'], false, status))
              });
            });
        })
    });
  }

  chooseAccount(login: any): void {
    this.arrayOfAccount.forEach(x => x.IsSelected = false);
    let selectedAccount = this.arrayOfAccount.find(x => x.Email === login);
    selectedAccount.IsSelected = true;

    this.powertToFlyService.getBotSinglsByAccountId(selectedAccount.Id, 10).subscribe(result => 
      { 
        this.arrayOfBotSignals = [];
        result.map(x =>  {
          let status = this.getBotSignalEnumValue(x['status']);
          let botSignal: BotSignal = new BotSignal();
          botSignal.Id = +x['id'];
          botSignal.JobLink = x['jobLink'];
          botSignal.SendedJob = x['sendedJobCount'];
          botSignal.FoundJob = x['allJobCount'];
          botSignal.Date = x['updatedAt'];
          botSignal.Status = status;
          botSignal.IsSelected = false;
          this.arrayOfBotSignals.push(botSignal);
        });
      });
  }

  chooseBotSignal(id: number): void {
    this.arrayOfBotSignals.forEach(x => x.IsSelected = false);
    let selectedBotSignal = this.arrayOfBotSignals.find(x => x.Id === id);

    selectedBotSignal.IsSelected = true;

    this.powertToFlyService.getSingleBotSignal(selectedBotSignal.Id).subscribe(result => 
      { 
        result.map(x =>  {
          let status = this.getBotSignalEnumValue(x['status']);
          selectedBotSignal.Id = +x['id'];
          selectedBotSignal.JobLink = x['jobLink'];
          selectedBotSignal.SendedJob = x['sendedJobCount'];
          selectedBotSignal.FoundJob = x['allJobCount'];
          selectedBotSignal.Date = x['updatedAt'];
          selectedBotSignal.Status = status;
          selectedBotSignal.IsSelected = false;
        });
      });


    this.selectedBotSignal = selectedBotSignal;
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

  getBotSignalEnumValue(number: number) : BotSignalStatus
  {
    let stauts;
    switch (number) {
      case number: 1
      stauts = BotSignalStatus[number];
        break;
      case number: 2
      stauts = BotSignalStatus[number];
        break;
      case number: 3
      stauts = BotSignalStatus[number];
        break;
      default:
        break;
    }

    return stauts;
  }

  renderStatusColor(status: string)
  {
    switch (status) {
      case 'InProgress':
        return 'avialable-status'
        break;
      case 'Waiting':
        return 'working-status'
        break;
      case 'Finished':
        return 'finished-status'
        break;
      default:
        break;
    }
  }
}
