import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-company-info',
  templateUrl: './company-info.component.html',
  styleUrls: ['./company-info.component.scss']
})
export class CompanyInfoComponent implements OnInit {
  @Input() dataToImport = {country: 'Poland', companyName: 'TC', email: 'test@test'};
  @Input() isVertical = false;
  @Input() companyName = 'Testing Center';
  @Input() email = 'default@email.com';
  @Input() country = 'Poland';

  // tslint:disable-next-line: new-parens
  @Output() emitYear = new EventEmitter<number>();
  constructor() {}

  ngOnInit() {
  }

}
