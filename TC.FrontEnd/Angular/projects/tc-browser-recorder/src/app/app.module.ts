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
import {MatDialogModule} from '@angular/material/dialog';
import { RunTestComponent } from './run-test/run-test.component';
import { HeaderComponent } from './header/header.component';
import { SelectBrowserEngineComponent } from './select-browser-engine/select-browser-engine.component';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { SaveTestModalComponent } from './save-test-modal/save-test-modal.component';
import {MatFormFieldModule} from '@angular/material/form-field';



@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    LoginComponent,
    OperatorItemComponent,
    RunTestComponent,
    HeaderComponent,
    SelectBrowserEngineComponent,
    SaveTestModalComponent
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
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatFormFieldModule
  ],
  providers: [
    AuthGuard
  ],
  entryComponents: [
    SaveTestModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
