import { Component, OnInit } from '@angular/core';
import { Service } from 'src/app/_services/app.service';
import { StorageService } from '../_services/storage.service';
import { Report } from '../_models/report';
import DataGrid from "devextreme/ui/data_grid";
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  reportsData: Report[];
  ActionsExpanded: boolean;
  ReportExpanded: boolean;
  dataGridInstance: DataGrid;

  constructor(private storageService: StorageService, private authService: AuthenticationService) { }

  ngOnInit() {
    if (!this.storageService.areReportsFetched) {
      let user = this.authService.currentUserValue;
      this.storageService.setEventReportData(user.roleId, user.associateId);
    }

    this.storageService.getReportData.subscribe(response => {
      this.reportsData = response;
    });
    
    this.ActionsExpanded = true;
    this.ReportExpanded = true;
  }

  completedValue(rowData) {
      return rowData.Status == "Completed";
  }

  ToggleCard(cardName: string, action: number) {
    if (cardName === 'actions') {
        this.ActionsExpanded = action === 1 ? true : false;
    } else
    if (cardName === 'report') {
        this.ReportExpanded = action === 1 ? true : false;
    }
  }

  saveGridInstance (e) {
    this.dataGridInstance = e.component;
  }

  clearFilterGrid() {
    this.dataGridInstance.clearFilter();
  }

  onToolbarPreparing(e){
    e.toolbarOptions.items.unshift({
      name: 'clearFiltersBtn',
      location: 'after',
      widget: 'dxButton',
      options: {
        hint: 'Clear Filters',
        icon: 'fa fa-times',
        text: 'Clear Filters',
        onClick: this.clearFilterGrid.bind(this)
      }
    });
  }

  SendMailToAssociate() {}
}
