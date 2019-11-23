import { Component, OnInit } from '@angular/core';
import { HttpClientService } from '../../../projects/shared/src/lib/services/http-client.service';
import { ProjectViewModel } from '../../../projects/shared/src/lib/viewModels/project-view-model';

@Component({
  selector: 'app-project-layout',
  templateUrl: './project-layout.component.html',
  styleUrls: ['./project-layout.component.scss']
})
export class ProjectLayoutComponent implements OnInit {
  projects: ProjectViewModel[];
  displayedColumns: string[] = ['name','edit','send test'];

  constructor(private httpClient: HttpClientService) { }

  ngOnInit() {
    this.httpClient.get('project').toPromise().then((projects: ProjectViewModel[]) => {
      this.projects = projects;
    });
  }

}
