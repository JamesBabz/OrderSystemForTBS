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
import {LoginComponent} from './login/login/login.component';
import {LoginService} from './login/shared/login.service';

const appRoutes: Routes = [
  {path: 'customers/:id', component: CustomerDetailComponent},
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
    path: '**', redirectTo: 'login'
  }
];

@NgModule({
  declarations: [
    AppComponent,
    CustomerListComponent,
    CustomerDetailComponent,
    CustomerComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule

  ],
  providers: [CustomerService, LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
