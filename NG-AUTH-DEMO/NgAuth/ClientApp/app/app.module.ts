import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AlertComponent } from './_directives/alert.component';
import { AlertService } from './_services/alert.service';
import { Routes, RouterModule } from '@angular/router';

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
      RouterModule.forRoot([
          { path: 'home', component: HomeComponent },
          { path: 'login', component: LoginComponent },
          { path: '', redirectTo: 'home', pathMatch: 'full' }
      ])
  ],
  providers: [ AlertService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
