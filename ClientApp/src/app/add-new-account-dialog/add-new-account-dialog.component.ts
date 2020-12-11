import { Component, Inject, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-add-new-account-dialog',
  templateUrl: './add-new-account-dialog.component.html',
  styleUrls: ['./add-new-account-dialog.component.css']
})
export class AddNewAccountDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<AddNewAccountDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}