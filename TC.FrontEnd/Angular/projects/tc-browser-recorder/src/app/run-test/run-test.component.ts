import { OperatorModelStatus } from './../../../../shared/src/lib/models/operatorModel';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SignalSzwagierService } from '../../../../shared/src/lib/services/signalr/signal-szwagier.service';
import { SzwagierModel } from '../../../../shared/src/lib/models/szwagierModel';
import { SzwagierType } from '../../../../shared/src/lib/models/SzwagierType';
import { StoreService } from '../services/store.service';
import { OperatorModel } from 'projects/shared/src/lib/models/operatorModel';
import { MatTableDataSource } from '@angular/material/table';
import { TestProgressMessage } from 'projects/shared/src/lib/models/TestProgressMessage';

@Component({
  selector: 'app-run-test',
  templateUrl: './run-test.component.html',
  styleUrls: ['./run-test.component.scss']
})
export class RunTestComponent implements OnInit {
  hubConnection: signalR.HubConnection;
  commands: OperatorModelStatus[];

  dataSource: MatTableDataSource<OperatorModelStatus>;
  displayedColumns = ['action', 'path', 'value', 'progress'];


  constructor(private storeService: StoreService, signalSzwagierService: SignalSzwagierService) {
    this.hubConnection = signalSzwagierService.start();
  }
  ngOnInit() {
    this.commands = this.storeService.getOperatorsData();
    this.dataSource = new MatTableDataSource<OperatorModelStatus>(this.commands);

  }
  sendClick() {

    var data = [];

    const operatorsData = this.storeService.getOperatorsData();

    for (let i = 0; i < operatorsData.length; i++) {
      const row = operatorsData[i];

      switch (row.action) {
        case 'goToUrl':
          data.push({
            operationId: 3, webDriverOperationType: 4, values: [row.value], guid: row.guid
          });
          break;
        case 'click':
          data.push({
            operationId: 0, webDriverOperationType: 5, values: [row.path], guid: row.guid
          });
          break;
        case 'sendKeys':
          data.push({
            operationId: 1, webDriverOperationType: 5, values: [row.path, row.value], guid: row.guid
          });
          break;
        case 'selectByValue':
          data.push({
            operationId: 2, webDriverOperationType: 5, values: [row.path, row.value], guid: row.guid
          });
          break;
      }
    }
    // close browser
    data.push({
      operationId: 18, webDriverOperationType: 4
    });
    const message = {
      ReceiverConnectionId: this.storeService.getSelectedBrowserEngine().connectionId,

      Commands: data
    }

    this.hubConnection.invoke('SendCommand', message);
    this.startTestProgressMonitor();
  }
  startTestProgressMonitor() {
    this.dataSource.data[0].status = 'inprogess';
    this.hubConnection.on('TestProgress', (testProgressMessage: TestProgressMessage) => {
      const test = this.dataSource.data.find(x => x.guid === testProgressMessage.commandTestGuid);
      if (testProgressMessage.isSuccesful) {
        test.status = 'done';
        const currentIndex = this.dataSource.data.findIndex(x => x.guid === testProgressMessage.commandTestGuid);
        this.dataSource.data[currentIndex].status = 'inprogess';
      } else {
        test.status = 'failed';
      }
      this.dataSource._updateChangeSubscription();
    });
  }


}
