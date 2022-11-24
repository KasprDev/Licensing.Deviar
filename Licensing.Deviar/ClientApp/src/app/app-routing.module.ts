import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { MySoftwareComponent } from './pages/my-software/my-software.component';
import { ViewSoftwareComponent } from './pages/my-software/view-software/view-software.component';
import { UsageLogsComponent } from './pages/usage-logs/usage-logs.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'software', component: MySoftwareComponent },
  { path: 'software/:id', component: ViewSoftwareComponent },
  { path: 'usage/:key', component: UsageLogsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
