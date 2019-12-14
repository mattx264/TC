import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../layout/layout.service';

@Component({
  selector: 'app-privacy-policy',
  templateUrl: './privacy-policy.component.html',
  styleUrls: ['./privacy-policy.component.scss']
})
export class PrivacyPolicyComponent implements OnInit {
  country: string;
  constructor(private layoutService: LayoutService) {
    this.country = 'Polandia';
   }

  ngOnInit() {
    this.layoutService.hideSidebarHeader();
    console.log('oninit');
  }

}
