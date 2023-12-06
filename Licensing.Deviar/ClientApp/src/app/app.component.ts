import { Component } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isCollapsed = false;

  constructor(private modal: NzModalService) {}

  loggedIn(): boolean {
    if (localStorage.getItem('token'))
      return true;
    else
      return false;
  }

  async logOut() {
    await this.modal.warning({
      nzTitle: 'Log Out',
      nzContent: 'Are you sure you want to log out?',
      nzOkDanger: true,
      nzOkText: 'Log Out',
      nzOnOk: () => {
        localStorage.clear();
        location.reload();
      }
    });
  }
}
