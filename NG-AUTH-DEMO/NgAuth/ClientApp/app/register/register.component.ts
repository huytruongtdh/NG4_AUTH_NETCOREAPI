import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertService } from '../_services/alert.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    model: any = {};
    loading = false;
    constructor(private userService: UserService, private alertService: AlertService) { }

    ngOnInit() {
    }

    register(firstName: any) {
        this.loading = true;
        let _this = this;
        this.userService.create(this.model)
            .subscribe(function (data) {
                // set message and pass true parameter to persist the message after redirecting to the login page
                _this.alertService.success('Registration successful', true);
            }, function (error) {
                alert('error');
            });
    }

}
