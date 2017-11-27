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
import {FormsModule} from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {LoginComponent} from './login/login/login.component';
import {LoginService} from './login/shared/login.service';
import { CustomerCreateComponent } from './customers/customer-create/customer-create.component';

const appRoutes: Routes = [
  {path: 'customer/:id', component: CustomerDetailComponent},
  {path: 'customers/create', component: CustomerCreateComponent},
  { path: 'login', component: LoginComponent },
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
    CustomerCreateComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    NgbModule

  ],
  providers: [CustomerService, LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
