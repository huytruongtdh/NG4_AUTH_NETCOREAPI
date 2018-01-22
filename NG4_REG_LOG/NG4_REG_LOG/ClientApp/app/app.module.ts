import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AlertComponent } from './_directives/alert.component';
import { AlertService } from './_services/alert.service';
import { JwtComponent } from './_helpers/jwt/jwt.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AlertComponent,
    JwtComponent,
  ],
  imports: [
    BrowserModule
  ],
  providers: [ AlertService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
