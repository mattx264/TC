import { EmailExistsValidator } from './../validators/email-exists-validator';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LayoutService } from '../../layout/layout.service';
import { AuthService } from '../../../../projects/shared/src/lib/services/auth/auth.service';
import { HttpClientService } from '../../../../projects/shared/src/lib/services/http-client.service';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private httpClientService: HttpClientService,
    private route: ActivatedRoute,
    private router: Router,
    layoutService: LayoutService
  ) {
    layoutService.hideSidebarHeader();
  }

  ngOnInit() {
    this.buildForm();
    this.authService.logout();
  }

  buildForm() {
    this.registerForm = this.fb.group({

      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email], EmailExistsValidator.emailExists(this.httpClientService)],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6)])],
    });
  }

  registerNewUser() {
    const userToCreate = this.registerForm.value;
    userToCreate.timeZone = new Date();

    this.httpClientService.post('user/Registration', userToCreate).subscribe((response: any) => {
      this.router.navigate(['']);
    });
  }

}