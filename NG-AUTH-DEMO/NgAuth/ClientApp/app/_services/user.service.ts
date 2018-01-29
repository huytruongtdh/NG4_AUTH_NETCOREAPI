import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class UserService {
    create(user: any): Observable<any> {
        console.log(user);
        return of(user);
    }

    constructor() { }
}
