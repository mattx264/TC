import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../layout/layout.service';

@Component({
  selector: 'app-privacy-policy',
  templateUrl: './privacy-policy.component.html',
  styleUrls: ['./privacy-policy.component.scss']
})
export class PrivacyPolicyComponent implements OnInit {

  constructor(private layoutService: LayoutService) {
    
   }

  ngOnInit() {
    this.layoutService.hideSidebarHeader();
    console.log('oninit');
  }

}
