import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CustomerListComponent } from './customers/customer-list/customer-list.component';
import {RouterModule, Routes} from '@angular/router';
import { HttpModule } from '@angular/http';
import { CustomerDetailComponent } from './customers/customer-detail/customer-detail.component';
import {HttpClientModule} from '@angular/common/http';
import {CustomerService} from './customers/shared/customer.service';
import { CustomerComponent } from './customers/customer/customer.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {LoginComponent} from './login/login/login.component';
import {LoginService} from './login/shared/login.service';
import { CustomerCreateComponent } from './customers/customer-create/customer-create.component';
import {TabModule} from 'angular-tabs-component';
import {AuthGuard} from './login/login/Auth/auth.guard';
import { PropositionComponent } from './propositions/proposition/proposition.component';
import { PropositionDetailComponent } from './propositions/proposition-detail/proposition-detail.component';
import { PropositionListComponent } from './propositions/proposition-list/proposition-list.component';
import {PropositionService} from './propositions/shared/proposition.service';

const appRoutes: Routes = [
  {path: 'customer/:id', component: CustomerDetailComponent},
  {path: 'customers/create', component: CustomerCreateComponent},
  {path: 'proposition/:id', component: PropositionDetailComponent},
  { path: 'login', component: LoginComponent },
  { path: '', component: LoginComponent, canActivate: [AuthGuard] },
  {
    path: 'customers',
    component: CustomerListComponent,
    data: {title: 'Customer list'}
  },
  {
    path: 'customers',
    redirectTo: '/customers',
    pathMatch: 'full'
  },
  {
    path: '**', redirectTo: 'customers'
  }
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
    PropositionListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    TabModule

  ],
  providers: [CustomerService, LoginService, AuthGuard, PropositionService],
  bootstrap: [AppComponent]
})
export class AppModule { }
