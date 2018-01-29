import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router, NavigationStart } from '@angular/router';

@Injectable()
export class AlertService {
    private subject = new Subject<any>();
    private keepAfterNavChange = false;

    constructor(private router: Router) {
        // clear alert message on route change
        router.events.subscribe(e => {
            // WHAT THIS???!
            if (event instanceof NavigationStart) {
                if (this.keepAfterNavChange) {
                    // only keep for a single location change
                    this.keepAfterNavChange = false;
                } else {
                    // clear alert
                    this.subject.next();
                }
            }
        });
    }

    success(message: string, keepAfterNavChange = false): any {
        this.keepAfterNavChange = keepAfterNavChange;
        this.subject.next({ type: 'success', text: message });
    }

    getMessage(): any {
        return "hellow this is my first message";
    }
}
