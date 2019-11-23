import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { SignalSzwagierService } from '../services/signal-szwagier.service';
import { SzwagierType, SzwagierTypeLabel } from '../../../projects/shared/src/lib/models/SzwagierType';
import { SzwagierModel } from '../../../projects/shared/src/lib/models/szwagierModel';
@Component({
  selector: 'app-szwagier-dashboard',
  templateUrl: './szwagier-dashboard.component.html',
  styleUrls: ['./szwagier-dashboard.component.scss']
})
export class SzwagierDashboardComponent implements OnInit {
  loading: boolean;
  displayedColumns: string[] = ['name', 'szwagierType', 'connectionId', 'location', 'userId', 'RC', 'Send test'];
  szwagiers: SzwagierModel[];
  hubConnection: signalR.HubConnection;
  constructor(private signalSzwagierService: SignalSzwagierService, private cd: ChangeDetectorRef) {

  }
  ngOnInit() {
    this.hubConnection = this.signalSzwagierService.start();
    this.hubConnection.on('UpdateSzwagierList', (data: SzwagierModel[]) => {
      this.szwagiers = [];
      if (data == null) {
        return;
      }
      data.forEach(element => {
        if (element.szwagierType !== SzwagierType.SzwagierDashboard) {
          element.szwagierTypeLabel=SzwagierTypeLabel.get(element.szwagierType);
          this.szwagiers.push(element);
        }
      });
      this.cd.detectChanges();

    });
  }
  sendTestClick() {
    //this.hubConnection.invoke('SendTriggerTest', 1);
  }

}
