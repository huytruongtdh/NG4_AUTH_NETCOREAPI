import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

    currentUser: User = new User();
    users: User[] = [];

    constructor() { }

    ngOnInit() {
        this.currentUser.firstName = 'Huy';
        this.currentUser.lastName = 'Truong';
        this.currentUser.userName = 'drgn7676';
    }
}
