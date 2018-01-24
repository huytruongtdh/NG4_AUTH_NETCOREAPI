import { Component, OnInit } from '@angular/core';

@Component({
    //moduleId: module.id, // example code
    selector: 'app-root',
    templateUrl: 'app.component.html',
    styles: []
})
export class AppComponent {
    title = 'app';

    constructor() {
        alert('xin chao');
    }
}
