import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isCollapsed = false;

  loggedIn(): boolean {
    if (localStorage.getItem('token'))
      return true;
    else
      return false;
  }
}
