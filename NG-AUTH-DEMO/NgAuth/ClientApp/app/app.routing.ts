import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";

export const routing = RouterModule
    .forRoot([
        { path: 'home', component: HomeComponent }, //{ path: '', component: HomeComponent, canActivate: [AuthGuard] },
        { path: '', redirectTo: '/home', pathMatch: 'full' },
        { path: 'login', component: LoginComponent },
        { path: 'register', component: RegisterComponent },
        // otherwise redirect to home // : to empty > to home
        { path: '**', redirectTo: '' }
    ], {
        useHash: true,
        enableTracing: false // for Debugging of the Routes
    });