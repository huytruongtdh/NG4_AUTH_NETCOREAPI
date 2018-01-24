import { Injectable } from '@angular/core';

@Injectable()
export class AlertService {
    getMessage(): any {
        return "hellow this is my first message";
    }

  constructor() { }

}
