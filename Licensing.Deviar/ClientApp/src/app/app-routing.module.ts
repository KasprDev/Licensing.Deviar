import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { MySoftwareComponent } from './pages/my-software/my-software.component';
import { ViewSoftwareComponent } from './pages/my-software/view-software/view-software.component';
import { UsageLogsComponent } from './pages/usage-logs/usage-logs.component';

const routes: Routes = [
  { path: '', component: MySoftwareComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'software', component: MySoftwareComponent, canActivate: [AuthGuard] },
  { path: 'software/:id', component: ViewSoftwareComponent, canActivate: [AuthGuard] },
  { path: 'usage/:key', component: UsageLogsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
