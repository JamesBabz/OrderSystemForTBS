import {BrowserModule} from '@angular/platform-browser';
import {ErrorHandler, NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {CustomerListComponent} from './customers/customer-list/customer-list.component';
import {RouterModule, Routes} from '@angular/router';
import {HttpModule} from '@angular/http';
import {CustomerDetailComponent} from './customers/customer-detail/customer-detail.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {CustomerService} from './customers/shared/customer.service';
import {CustomerComponent} from './customers/customer/customer.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {LoginComponent} from './login/login/login.component';
import {LoginService} from './login/shared/login.service';
import {CustomerCreateComponent} from './customers/customer-create/customer-create.component';
import {TabModule} from 'angular-tabs-component';
import {AuthGuard} from './login/login/Auth/auth.guard';
import {PropositionComponent} from './propositions/proposition/proposition.component';
import {PropositionListComponent} from './propositions/proposition-list/proposition-list.component';
import {PropositionService} from './propositions/shared/proposition.service';
import {TokenInterceptor} from './login/login/Auth/token.interceptor';
import {PropositionCreateComponent} from './propositions/proposition-create/proposition-create.component';
import {EquipmentComponent} from './equipment/equipment/equipment.component';
import {EquipmentListComponent} from './equipment/equipment-list/equipment-list.component';
import {EquipmentService} from './equipment/shared/equipment.service';
import {VisitListComponent} from './visits/visit-list/visit-list.component';
import {VisitCreateComponent} from './visits/visit-create/visit-create.component';
import {VisitComponent} from './visits/visit/visit.component';
import {EmployeeService} from './login/shared/employee.service';
import {VisitService} from './visits/shared/visit.service';
import {DawaService} from './customers/shared/dawa.service';
import {ErrorInterceptor} from './login/login/Auth/error.interceptor';
import {CVRService} from './customers/shared/cvr.service';
import {CalendarsComponent} from './calendar/calendars/calendars.component';
import {CalendarService} from './calendar/shared/calendar.service';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {FullCalendarModule} from 'ng-fullcalendar';
import {SalesmanListService} from './customers/shared/salesman-list.service';
import {EmployeeCreateComponent} from './employee/employee-create/employee-create.component';
import {PasswordResetComponent} from './login/login/password-reset/password-reset.component';
import {AdminComponent} from './admin/admin/admin.component';
import {EmployeeComponent} from './employee/employee/employee.component';
import {EmployeeDetailComponent} from './employee/employee/employee-detail/employee-detail.component';
import {NotificationModule} from 'angular-ntf';
import {SimpleNotificationsModule} from 'angular2-notifications';
import { ReceiptComponent } from './receipts/receipt/receipt.component';
import { ReceiptCreateComponent } from './receipts/receipt-create/receipt-create.component';
import { ReceiptListComponent } from './receipts/receipt-list/receipt-list.component';
import {ReceiptService} from './receipts/shared/receipt.service';
import {SharedService} from './shared/shared.service';
import { VisitUpdateComponent } from './visits/visit-update/visit-update.component';

const appRoutes: Routes = [

  {path: 'customer/:id', component: CustomerDetailComponent},
  {path: 'employee/:id', component: EmployeeDetailComponent},
  {path: 'customers/create', component: CustomerCreateComponent},
  {path: 'employees/create', component: EmployeeCreateComponent},
  {path: 'propositions/create', component: PropositionCreateComponent},
  {path: 'visits/create', component: VisitCreateComponent},
  {path: 'login', component: LoginComponent},
  {path: 'passwordreset/:id', component: PasswordResetComponent},
  {path: 'customers', component: CustomerListComponent},
  {path: 'calendar', component: CalendarsComponent},
  {path: 'admin', component: AdminComponent, canActivate: [AuthGuard]},
  {path: 'receipts/create', component: ReceiptCreateComponent},

  {path: '**', redirectTo: 'customers'}
];

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    CustomerListComponent,
    CustomerDetailComponent,
    CustomerComponent,
    LoginComponent,
    EmployeeDetailComponent,
    CustomerCreateComponent,
    PropositionComponent,
    PropositionListComponent,
    PropositionCreateComponent,
    EquipmentComponent,
    EquipmentListComponent,
    VisitListComponent,
    VisitCreateComponent,
    VisitComponent,
    CalendarsComponent,
    EmployeeCreateComponent,
    PasswordResetComponent,
    EmployeeComponent,
    ReceiptComponent,
    ReceiptCreateComponent,
    ReceiptListComponent,
    VisitUpdateComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    NgbModule.forRoot(),
    TabModule,
    BrowserModule,
    FullCalendarModule
  ],

    providers: [CustomerService, LoginService, AuthGuard, PropositionService,
  EquipmentService, EmployeeService, VisitService, DawaService, CalendarService, CVRService, SalesmanListService, ReceiptService, SharedService,
  {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
  {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
],
  bootstrap: [AppComponent]
})


export class AppModule {
}
