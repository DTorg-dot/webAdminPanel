import { FlexJobService } from './services/flexjob.service';
import { Observable } from 'rxjs';
import { PowerToFlyService } from './services/power-to-fly.service';
import { AccountPowerToFly, AccountStatus } from './modelsForService/AccountPowerToFly';
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

  constructor(public dialog: MatDialog, public powertToFlyService: PowerToFlyService, public flexJobService: FlexJobService, private _snackBar: MatSnackBar) {}

  ngOnInit(): void {

  }
}
