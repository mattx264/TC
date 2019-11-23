import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { AuthGuard } from './auth/guard/auth.guard';
import { LoginComponent } from './auth/login/login.component';
import { ProjectResolver } from './services/resolvers/project-resolver';



const routes: Routes = [
  {
    path: 'login', component: LoginComponent
  },
  {
    path: '**', component: LandingPageComponent
    , canActivate: [AuthGuard]
    , resolve: {
      project: ProjectResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
