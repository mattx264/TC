import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Inject } from '@angular/core';
import { HttpClientService } from 'projects/shared/src/lib/services/http-client.service';

@Component({
  selector: 'app-save-test-modal',
  templateUrl: './save-test-modal.component.html',
  styleUrls: ['./save-test-modal.component.scss']
})
export class SaveTestModalComponent implements OnInit {
  formGroupp: FormGroup;
  constructor(
    private dialogRef: MatDialogRef<SaveTestModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private httpClient: HttpClientService
  ) { }

  ngOnInit() {
    this.formGroupp = this.buildForm();
  }
  buildForm(): FormGroup {
    return this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }
  saveTest() {
    // if (this.formGroup.invalid) {
    //   return;
    // }
    // const data = this.formGroup.getRawValue();
    // data.projectId = '??????';
    // data.seleniumCommands;
    // this.httpClient.post('projectTest', this.formGroup.getRawValue()).subscribe(response => {
    //   alert("Save successful");
    // });
  }

}
