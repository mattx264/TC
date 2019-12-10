import { Component, OnInit } from '@angular/core';
import { SzwagierModel } from 'projects/shared/src/lib/models/szwagierModel';
import { SignalSzwagierService } from 'projects/shared/src/lib/services/signalr/signal-szwagier.service';
import { StoreService } from '../services/store.service';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { SzwagierType } from 'projects/shared/src/lib/models/SzwagierType';

@Component({
  selector: 'app-select-browser-engine',
  templateUrl: './select-browser-engine.component.html',
  styleUrls: ['./select-browser-engine.component.scss']
})
export class SelectBrowserEngineComponent implements OnInit {
  hubConnection: signalR.HubConnection;
  szwagiersConsoles: SzwagierModel[];
  selectedSzwagierConsole: SzwagierModel;
  constructor(signalSzwagierService: SignalSzwagierService, private storeService: StoreService, private router: Router
    ,         private route: ActivatedRoute) {
    this.hubConnection = signalSzwagierService.start();
  }

  ngOnInit() {
    console.log(this.route.snapshot.queryParams.returnUrl)
    this.hubConnection.on('UpdateSzwagierList', (data: SzwagierModel[]) => {
      if (data == null) {
        return;
      }
      this.szwagiersConsoles = data.filter(x => x.szwagierType === SzwagierType.SzwagierConsole);
    });
  }
  saveTestClick() {

  }
  backClick() {
    this.router.navigate(['**']);
  }
  sendClick(index: number) {
    // opend modal
    this.selectedSzwagierConsole = this.szwagiersConsoles[index];

    var data = [];

    const operatorsData = this.storeService.getOperatorsData();

    for (let i = 0; i < operatorsData.length; i++) {
      const row = operatorsData[i];

      switch (row.action) {
        case 'goToUrl':
          data.push({
            operationId: 3, webDriverOperationType: 4, values: [row.value]
          });
          break;
        case 'click':
          data.push({
            operationId: 0, webDriverOperationType: 5, values: [row.path]
          });
          break;
        case 'sendKeys':
          data.push({
            operationId: 1, webDriverOperationType: 5, values: [row.path, row.value]
          });
          break;
        case 'selectByValue':
          data.push({
            operationId: 2, webDriverOperationType: 5, values: [row.path, row.value]
          });
          break;
      }
    }
    // close browser
    data.push({
      operationId: 18, webDriverOperationType: 4
    });
    const message = {
      ReceiverConnectionId: this.selectedSzwagierConsole.connectionId,

      Commands: data
    }

    this.hubConnection.invoke('SendCommand', message);
  }

}
