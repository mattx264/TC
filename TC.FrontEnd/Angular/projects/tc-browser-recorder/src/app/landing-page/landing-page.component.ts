import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { SignalSzwagierService } from '../../../../shared/src/lib/services/signalr/signal-szwagier.service';
import { SzwagierModel } from '../../../../shared/src/lib/models/szwagierModel';
import { SzwagierType } from '../../../../shared/src/lib/models/SzwagierType';
import { HttpClientService } from '../../../../shared/src/lib/services/http-client.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ProjectViewModel } from '../../../../shared/src/lib/models/project/projectViewModel';
import { ProjectDomainViewModel } from '../../../../shared/src/lib/models/project/projectDomainViewModel';
import { AuthService } from '../../../../shared/src/lib/services/auth/auth.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {
  hubConnection: signalR.HubConnection;
  operatorsData: { action: string, path: string, value: string }[] = [];
  szwagiersConsoles: SzwagierModel[] = [];
  selectedSzwagierConsole: SzwagierModel;
  isStarted: boolean = false;
  display: boolean = false;
  projects: ProjectViewModel[];
  domain: string;
  project: ProjectViewModel;
  projectDomain: ProjectDomainViewModel;
  constructor(private signalSzwagierService: SignalSzwagierService, route: ActivatedRoute,
    private cdr: ChangeDetectorRef, private authService: AuthService, private router: Router) {
    route.data.subscribe((res: any) => {
      this.projects = res.project;
    });
    this.hubConnection = signalSzwagierService.start();
  }
  ngOnInit() {
    chrome.runtime.onMessage.addListener(
      (request, sender, sendResponse) => {
        if (this.isStarted === false) {
          if (request.action === "goToUrl") {
            var matches = request.value.match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);
            this.domain = matches && matches[1];
            this.domain = this.domain.replace('www.', '');
            this.projects.forEach(p => {
              if (p.projectDomain.find(x => x.domain === this.domain)) {
                this.project = p;
                this.projectDomain = p.projectDomain.find(x => x.domain === this.domain);
              }
            });
          }
          return;
        }
        this.operatorsData.push({
          action: request.action,
          path: request.xpath,
          value: request.value
        });
        this.cdr.detectChanges();

      });
    this.hubConnection.on('UpdateSzwagierList', (data: SzwagierModel[]) => {

      if (data == null) {
        return;
      }
      this.szwagiersConsoles = data.filter(x => x.szwagierType === SzwagierType.SzwagierConsole);
    });
    this.sendMessageToBrowser('getUrl');
  }

  startRecordingClick() {
    this.isStarted = true;
    this.sendMessageToBrowser('getUrl');
  }
  saveClick() {

  }
  sendClick() {
    // opend modal
    this.selectedSzwagierConsole = this.szwagiersConsoles[0];
    if (this.selectedSzwagierConsole == null) {
      this.display = true;
      return;
    }
    var data = [];
    //   var table = document.getElementById("actionTable") as HTMLTableElement;
    //  var rows = table.tBodies[0].rows;
    for (let i = 0; i < this.operatorsData.length; i++) {
      const row = this.operatorsData[i];

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
    })
    this.hubConnection.invoke('SendCommand', data);
  }
  restartClick() {
    this.operatorsData = [];
    this.sendMessageToBrowser('getUrl');

  }
  removeOperatorItem(index: number) {
    this.operatorsData.splice(index, 1);
    this.cdr.detectChanges();
  }
  sendMessageToBrowser(methodName) {
    var id = location.href.substr(location.href.indexOf("?") + 4);
    chrome.tabs.sendMessage(+id, { method: methodName }, function (response) {
      // reponse coming back by chrome.runtime.onMessage.addListener(
    });
  }
  createNewProject() {
    window.open("http://tc.net/project/create")
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
