import { Injectable } from '@angular/core';

import {
    MatSnackBar,
    MatSnackBarConfig,
    MatSnackBarHorizontalPosition,
    MatSnackBarVerticalPosition,
  } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class AlertService {

    message: any;
    verticalPosition: MatSnackBarVerticalPosition = 'top';
    horizontalPosition: MatSnackBarHorizontalPosition = 'center';
    setAutoHide: boolean = true;
    action: boolean = true;
    autoHide: number = 2000;
    actionButtonLabel: string = '';
    addExtraClass: boolean = true;

    constructor(public snackBar: MatSnackBar) {
    }
    successAlert(message: string) {
        let config = new MatSnackBarConfig();
        config.verticalPosition = this.verticalPosition;
        config.horizontalPosition = this.horizontalPosition;
        config.duration = this.setAutoHide ? this.autoHide : 0;
        config.panelClass  = ['success']
        this.snackBar.open(message, this.action ? this.actionButtonLabel : undefined, config);
    }
    errorAlert(message: string){
        let config = new MatSnackBarConfig();
        config.verticalPosition = this.verticalPosition;
        config.horizontalPosition = this.horizontalPosition;
        config.duration = this.setAutoHide ? this.autoHide : 0;
        config.panelClass  = ['error'];
        this.snackBar.open(message, this.action ? this.actionButtonLabel : undefined, config);
    }
}
