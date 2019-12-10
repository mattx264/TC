import { Component, OnInit } from '@angular/core';
import { StoreService } from '../services/store.service';
import { ProjectViewModel } from '../../../../shared/src/lib/models/project/projectViewModel';
import { Router } from '@angular/router';
import { AuthService } from '../../../../shared/src/lib/services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  project: ProjectViewModel;

  constructor(private storeService: StoreService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.storeService.state$.subscribe((x: ProjectViewModel) => {
      this.project = x;
    });
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
