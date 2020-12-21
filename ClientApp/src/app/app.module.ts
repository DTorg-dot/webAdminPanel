import { PowerToFlyService } from './services/power-to-fly.service';
import { AccountPowerToFly } from './modelsForService/AccountPowerToFly';
import { AccountStatus } from './modelsForService/AccountPowerToFly';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common'; 
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS} from '@angular/material/dialog';
import {MatCheckboxModule} from '@angular/material/checkbox';

import { AppComponent } from './app.component';
import { AddNewAccountDialogComponent } from './add-new-account-dialog/add-new-account-dialog.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatSnackBarModule} from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    AppComponent,
    AddNewAccountDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    MatTabsModule,
    NoopAnimationsModule,
    MatCardModule,
    MatSidenavModule,
    MatDialogModule,
    MatSnackBarModule,
    MatCheckboxModule
  ],
  providers: [
    {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}},
  ],
  entryComponents: [
    AddNewAccountDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
