import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { Router } from '@angular/router';
import { AlertService } from '../_services/alert.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    model: any = {};
    loading = false;
    returnUrl: string;

    constructor(
        //private route: ActivedRoute,
        private router: Router,
        private authService: AuthenticationService,
        private alertService: AlertService) { }

    ngOnInit() {
    }

    login() {
        console.log(this.model);

        this.loading = true;
        this.authService.login(this.model.userName, this.model.password)
            .subscribe(data => {
                alert('login success!');
                this.router.navigate(['/home']);
                //this.router.navigate(['']);
            }, error => {
                this.alertService.error(error);
                this.loading = false;
            });

    }
}
