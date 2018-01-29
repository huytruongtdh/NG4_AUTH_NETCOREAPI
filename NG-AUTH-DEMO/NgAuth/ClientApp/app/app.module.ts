import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AlertComponent } from './_directives/alert.component';
import { AlertService } from './_services/alert.service';
import { RouterModule } from '@angular/router';
import { routing } from './app.routing';

let routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AlertComponent,
  ],
  imports: [
      BrowserModule,
      routing
  ],
  //exports: [ RouterModule ],
  providers: [ AlertService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
