import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-company-info',
  templateUrl: './company-info.component.html',
  styleUrls: ['./company-info.component.scss']
})
export class CompanyInfoComponent implements OnInit {
  @Input() companyName = 'Testing Center';
  @Input() email = 'default@email.com';
  @Input() country = 'Poland';
  constructor() {

  }

  ngOnInit() {
  }

}
