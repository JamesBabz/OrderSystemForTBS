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
import {PropositionDetailComponent} from './propositions/proposition-detail/proposition-detail.component';
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
import {CalendarsComponent} from './calendar/calendars/calendars.component';
import {CalendarService} from './calendar/shared/calendar.service';
import { FullCalendarModule } from 'ng-fullcalendar';


const appRoutes: Routes = [

  {path: 'customer/:id', component: CustomerDetailComponent, canActivate: [AuthGuard]},
  {path: 'customers/create', component: CustomerCreateComponent, canActivate: [AuthGuard]},
  {path: 'proposition/:id', component: PropositionDetailComponent, canActivate: [AuthGuard]},
  {path: 'propositions/create', component: PropositionCreateComponent, canActivate: [AuthGuard]},
  {path: 'visits/create', component: VisitCreateComponent},
  {path: 'login', component: LoginComponent},
  {path: 'customers', component: CustomerListComponent, canActivate: [AuthGuard]},
  {path: 'calendar', component: CalendarsComponent, canActivate: [AuthGuard]},

  {path: '**', redirectTo: 'customers'}
];

@NgModule({
  declarations: [
    AppComponent,
    CustomerListComponent,
    CustomerDetailComponent,
    CustomerComponent,
    LoginComponent,
    CustomerCreateComponent,
    PropositionComponent,
    PropositionDetailComponent,
    PropositionListComponent,
    PropositionCreateComponent,
    EquipmentComponent,
    EquipmentListComponent,
    VisitListComponent,
    VisitCreateComponent,
    VisitComponent,
    CalendarsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    NgbModule.forRoot(),
    TabModule,
    BrowserModule,
    FullCalendarModule
  ],
  providers: [CustomerService, LoginService, AuthGuard, PropositionService,
    EquipmentService, EmployeeService, VisitService, DawaService, CalendarService,
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
  ],

  bootstrap: [AppComponent]
})
export class AppModule {
}
