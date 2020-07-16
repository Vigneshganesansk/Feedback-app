import { Component, OnInit } from '@angular/core';
import { StorageService } from '../_services/storage.service';
import { Report } from '../_models/report';
import { AuthenticationService } from '../_services/authentication.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private storageService: StorageService, private authService: AuthenticationService) { }

  sections: any;
  reportsData: Report[];

  ngOnInit() {
    if (!this.storageService.areReportsFetched) {
      let user = this.authService.currentUserValue;
      this.storageService.setEventReportData(user.roleId, user.associateId);
    }

    this.storageService.getReportData.subscribe(response => {
      this.reportsData = response;
      let Totals = this.getTotals();
      this.sections = [
        {
          title: 'Total Events',
          subtitle: Totals.Events,
          backgroundColor: 'rgb(218, 85, 80)',
        },
        {
          title: 'Lives Impacted',
          subtitle: Totals.LivesImpacted,
          backgroundColor: 'rgb(255, 142, 29)',
        },
        {
          title: 'Total Volunteers',
          subtitle: Totals.Volunteers,
          backgroundColor: 'rgb(194, 4, 154)',
        },
        {
          title: 'Total Participants',
          subtitle: Totals.Participants,
          backgroundColor: 'rgb(82, 85, 90)',
        },
      ];
    });
  }

  getTotals(): any {
    let Totals: any = {
      Events: 0,
      LivesImpacted: 0,
      Volunteers: 0,
      Participants: 0
    }

    this.reportsData.forEach(rpt => {
      Totals.Events = Totals.Events + 1;
      Totals.LivesImpacted = Totals.LivesImpacted + rpt.LivesImpacted;
      Totals.Volunteers = Totals.Volunteers + rpt.ParticipantList.length + rpt.NotParticipatedList.length;
      Totals.Participants = Totals.Participants + rpt.ParticipantList.length;
    });

    return Totals;
  }
}
