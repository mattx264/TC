
import { GuidGeneratorService } from './../../../../shared/src/lib/services/guid-generator.service';
import { Component, OnInit, ChangeDetectorRef, NgZone } from '@angular/core';
import { SzwagierModel } from '../../../../shared/src/lib/models/szwagierModel';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ProjectViewModel } from '../../../../shared/src/lib/models/project/projectViewModel';
import { ProjectDomainViewModel } from '../../../../shared/src/lib/models/project/projectDomainViewModel';
import { StoreService } from '../services/store.service';
import { OperatorModel } from '../../../../shared/src/lib/models/operatorModel';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {

  operatorsData: OperatorModel[] = [];
  szwagiersConsoles: SzwagierModel[] = [];
  selectedSzwagierConsole: SzwagierModel;
  isStarted: boolean = false;
  projects: ProjectViewModel[];
  domain: string;
  project: ProjectViewModel;
  projectDomain: ProjectDomainViewModel;
  formGroupp: any;
  constructor(route: ActivatedRoute, private ngZone: NgZone,
    private cdr: ChangeDetectorRef, private router: Router,
    private storeService: StoreService, private guidGeneratorService: GuidGeneratorService,
  ) {
    route.data.subscribe((res: any) => {
      this.projects = res.project;
    });

  }
  ngOnInit() {
    chrome.runtime.onMessage.addListener(
      (request: { type: string, data: OperatorModel }, sender, sendResponse) => {
        if (this.isStarted === false) {
          this.beforeStart(request.data);
          return;
        }
        if (request.type == 'insert') {
          this.addNewOperation(request.data);
        } else if (request.type == 'appendLastValue') {
          this.appendLastOperation(request.data);
        }
        this.cdr.detectChanges();
      });

    this.sendMessageToBrowser('getUrl');
  }

  startRecordingClick() {
    this.isStarted = true;
    this.sendMessageToBrowser('getUrl');
  }
  saveClick() {
    this.storeService.setOperatorsData(this.operatorsData);

    this.ngZone.run(() =>
      this.router.navigate(['/save-test'])
    );
  }
  sendClick() {
    this.storeService.setOperatorsData(this.operatorsData);
    this.router.navigate(['/select-browser-engine']);
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
    if (localStorage.getItem('tabId') == null) {
      var id = location.href.substr(location.href.indexOf("?") + 4);
      localStorage.setItem('tabId', id);
    }
    chrome.tabs.sendMessage(+localStorage.getItem('tabId'), { method: methodName }, function (response) {
      // reponse coming back by chrome.runtime.onMessage.addListener(
    });
  }
  createNewProject() {
    window.open("http://tc.net/project/create")
  }

  private addNewOperation(request: OperatorModel) {
    const newOperation: OperatorModel = {
      action: request.action,
      path: request.path,
      value: request.value,
      guid: this.guidGeneratorService.get()
    }
    switch (request.action) {
      case 'xhrStart':
      case 'xhrDone':
        newOperation.value = request.value.toString()
        break;
    }
    this.operatorsData.push(
      newOperation
    );
  }
  private appendLastOperation(request: OperatorModel) {
    if (this.operatorsData.length === 0) {
      throw new Error("You cannot call updateLastOperation() when operatorsData is empty.");
    }
    if (request.value === 'Keys.BACKSPACE') {
      this.operatorsData[this.operatorsData.length - 1].value =
        this.operatorsData[this.operatorsData.length - 1].value.slice(0, this.operatorsData[this.operatorsData.length - 1].value.length - 1);
      return;
    }
    this.operatorsData[this.operatorsData.length - 1].value += <string>request.value;
  }

  private beforeStart(request: OperatorModel) {
    if (request.action === "goToUrl") {
      var matches = (request.value as string).match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);
      this.domain = matches && matches[1];
      this.domain = this.domain.replace('www.', '');
      this.projects.forEach(p => {
        if (p.projectDomain.find(x => x.domain === this.domain)) {
          this.project = p;
          this.storeService.setProject(this.project);
          this.projectDomain = p.projectDomain.find(x => x.domain === this.domain);
          this.cdr.detectChanges();
        }
      });
    }
  }
}
