import { Component, OnInit } from '@angular/core';
import { StoreService } from '../services/store.service';
import { ProjectViewModel } from '../../../../shared/src/lib/models/project/projectViewModel';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  project: ProjectViewModel;

  constructor(private storeService: StoreService) { }

  ngOnInit() {
    this.storeService.state$.subscribe((x: ProjectViewModel) => {
      this.project = x;
    });
  }

}
