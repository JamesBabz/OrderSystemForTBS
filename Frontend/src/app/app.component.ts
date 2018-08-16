import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent {

  template: '<simple-notifications [options]="options"></simple-notifications>';

  public options = {
    position: ["bottom", "right"],
    timeOut: 3000,
    lastOnBottom: true,
    showProgressBar: true,
    maxStack: 3,
    preventDuplicates: true
  }
}
