import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClientService } from '../../../../projects/shared/src/lib/services/http-client.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-create',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.scss']
})
export class ProjectCreateComponent implements OnInit {
  formGroup: FormGroup;
  saveProjectEndPoint: string = 'project';

  constructor(private fb: FormBuilder, 
    private router: Router,
    private http: HttpClientService) { }

  ngOnInit() {
    this.formGroup = this.buildForm();
  }
  buildForm(): FormGroup {
    return this.fb.group({
      'name': ['', Validators.required],
      'description': ['', Validators.required],
      'domains': ['', Validators.required],
      'usersEmail': [''],
    });
  }
  save() {
    if (this.formGroup.invalid) {
      return;
    }
    
    this.http.post(this.saveProjectEndPoint, this.formGroup.getRawValue())
      .subscribe(
        r => this.router.navigate(['project']),
        e => alert(e.error)
    );
  }
}
