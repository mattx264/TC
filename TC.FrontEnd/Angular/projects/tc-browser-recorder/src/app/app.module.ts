import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './auth/login/login.component';
import { SharedModule } from '../../../../projects/shared/src/lib/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthGuard } from './auth/guard/auth.guard';
import { environment } from '../environments/environment';
import { OperatorItemComponent } from './operator-item/operator-item.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import {  MatCardModule } from '@angular/material/card';

@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    LoginComponent,
    OperatorItemComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule.forRoot(environment),
    FormsModule,
    ReactiveFormsModule,  
    MatButtonModule,
    MatInputModule,
    MatCardModule
  ],
  providers: [
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
