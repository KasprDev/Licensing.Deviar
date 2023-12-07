import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ApiService } from 'src/services/api.service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconsProviderModule } from './icons-provider.module';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { TokenizedInterceptor } from 'src/interceptors/http-interceptor';
import { LoginComponent } from './pages/login/login.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { MySoftwareComponent } from './pages/my-software/my-software.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { ViewSoftwareComponent } from './pages/my-software/view-software/view-software.component';
import { UsageLogsComponent } from './pages/usage-logs/usage-logs.component';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { ResellComponent } from './pages/resell/resell.component';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { ResellerListComponent } from './pages/reseller-list/reseller-list.component';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MySoftwareComponent,
    ViewSoftwareComponent,
    UsageLogsComponent,
    ResellComponent,
    ResellerListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    IconsProviderModule,
    NzLayoutModule,
    NzMenuModule,
    NzButtonModule,
    NzInputModule,
    NzDividerModule,
    NzModalModule,
    NzTableModule,
    NzDatePickerModule,
    NzDropDownModule,
    NzInputNumberModule,
    NzSpinModule
  ],
  providers: [
    ApiService,
    { provide: HTTP_INTERCEPTORS, useClass: TokenizedInterceptor, multi: true },
    { provide: NZ_I18N, useValue: en_US }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
