import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClientService } from '../../../../projects/shared/src/lib/services/http-client.service';

@Component({
  selector: 'app-project-create',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.scss']
})
export class ProjectCreateComponent implements OnInit {
  formGroup: FormGroup;

  constructor(private fb: FormBuilder, private httpClient: HttpClientService) { }

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
    this.httpClient.post('project', this.formGroup.getRawValue()).subscribe(response => {
      alert("Save successful");
    });
  }
}
