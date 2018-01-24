import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent }, // show home component as default
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);