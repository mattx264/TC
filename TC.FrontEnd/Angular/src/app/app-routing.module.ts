import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SzwagierDashboardComponent } from './szwagier-dashboard/szwagier-dashboard.component';
import { SzwagierRCComponent } from './szwagier-rc/szwagier-rc.component';
import { WebRtcComponent } from './web-rtc/web-rtc.component';
import { LoginComponent } from './auth/login/login.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { AuthGuard } from './auth/guard/auth.guard';
import { SidebarEmptyComponent } from './layout/sidebar/sidebar-empty/sidebar-empty.component';
import { GroupLayoutComponent } from './group-layout/group-layout.component';
import { GroupEditComponent } from './group-layout/group-edit/group-edit.component';
import { ProjectLayoutComponent } from './project-layout/project-layout.component';
import { ProjectCreateComponent } from './project-layout/project-create/project-create.component';
import { ProjectEditComponent } from './project-layout/project-edit/project-edit.component';
import { SendTestComponent } from './test-layout/send-test/send-test.component';
import { ServerNotAvaiableComponent } from '../../projects/shared/src/lib/components/server-not-avaiable/server-not-avaiable.component';
import { SimpleErrorPageComponent } from '../../projects/shared/src/lib/components/simple-error-page/simple-error-page.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';

const routes: Routes = [
  { path: 'szwagierDashboard', data: { showSidebar: true }, component: SzwagierDashboardComponent, canActivate: [AuthGuard]  },
  { path: 'szwagier-rc/:id', component: SzwagierRCComponent, canActivate: [AuthGuard]  },
  { path: 'webrtc', component: WebRtcComponent, canActivate: [AuthGuard]  },
  { path: 'group', component: GroupLayoutComponent, canActivate: [AuthGuard]  },
  { path: 'group/:id', component: GroupEditComponent, canActivate: [AuthGuard]  },
  { path: 'project', component: ProjectLayoutComponent, canActivate: [AuthGuard]  },
  { path: 'project-create', component: ProjectCreateComponent, canActivate: [AuthGuard]  },
  { path: 'project/:id', component: ProjectEditComponent, canActivate: [AuthGuard]  },
  { path: 'send-test/:projectId', component: SendTestComponent, canActivate: [AuthGuard]  }, 
  { path: 'send-test', component: SendTestComponent, canActivate: [AuthGuard]  }, 
  { path: 'login', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: '', redirectTo: 'szwagierDashboard', pathMatch: 'full'  },
  { path: '', component: SidebarEmptyComponent, outlet: 'sidebar' },
  { path: 'privacy-policy', component: PrivacyPolicyComponent, outlet: 'sidebar' },
  { path: 'server-not-avaiable', component: ServerNotAvaiableComponent  },
  { path: 'error', component: SimpleErrorPageComponent  },
  { path: '**', redirectTo: 'szwagierDashboard', pathMatch: 'full'  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
