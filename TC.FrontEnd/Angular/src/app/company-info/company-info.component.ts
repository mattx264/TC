import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-company-info',
  templateUrl: './company-info.component.html',
  styleUrls: ['./company-info.component.scss']
})
export class CompanyInfoComponent implements OnInit {
  @Input() isVertical: boolean = false;
  @Input() companyName: string = 'Testing Center';
  @Input() email: string = 'default@email.com';
  @Input() country: string = 'Poland';
  constructor() {

  }

  ngOnInit() {
  }

}
