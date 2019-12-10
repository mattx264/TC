import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SignalSzwagierService } from '../../../../shared/src/lib/services/signalr/signal-szwagier.service';
import { SzwagierModel } from '../../../../shared/src/lib/models/szwagierModel';
import { SzwagierType } from '../../../../shared/src/lib/models/SzwagierType';
import { StoreService } from '../services/store.service';

@Component({
  selector: 'app-run-test',
  templateUrl: './run-test.component.html',
  styleUrls: ['./run-test.component.scss']
})
export class RunTestComponent implements OnInit {

  constructor(private storeService: StoreService) {

  }
  ngOnInit() {

  }
  sendClick(index: number) {

    // opend modal

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