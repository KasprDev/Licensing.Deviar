import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public user: any = {};
  public loggingIn: boolean;

  constructor(private api: ApiService, private modal: NzModalService, private router: Router) { }

  ngOnInit(): void {

  }

  async login() {
    this.loggingIn = true;
    await this.api.login(this.user).then((resp) => {
      localStorage.setItem('token', resp.token);

      if (!resp.reseller) {
        this.router.navigate(['/software']);
      } else {
        localStorage.setItem('reseller', 'true');
        this.router.navigate(['/resell']);
      }
    })
    .catch(async (error) => {
      await this.modal.create({
        nzTitle: 'Oops!',
        nzContent: error.data.message
      });
    });
    this.loggingIn = false;
  }
}
