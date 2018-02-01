import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { AuthGuard } from "./_guards/auth.guard";

//var appRoutes = [
//    { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
//    { path: '', redirectTo: '/home', pathMatch: 'full' },
//    { path: 'login', component: LoginComponent },
//    { path: 'register', component: RegisterComponent },
//    // otherwise redirect to home // : to empty > to home
//    { path: '**', redirectTo: '' }
//];
//export const routing = RouterModule
//    .forRoot(appRoutes, {
//        useHash: true,
//        enableTracing: false // for Debugging of the Routes
//    });

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
export const routing = RouterModule.forRoot(appRoutes);