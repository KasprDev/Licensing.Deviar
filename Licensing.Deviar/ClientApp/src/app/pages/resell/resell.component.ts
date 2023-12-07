import { Component } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-resell',
  templateUrl: `./resell.component.html`,
  styleUrl: './resell.component.css'
})
export class ResellComponent { 
  public software: any[] = [];

  constructor(private api: ApiService, private modal: NzModalService) { }

  async ngOnInit() {
    this.software = await this.api.getResellerSoftware();
  }

  async createLink(data) {
    data.creating = true;
    await this.api.createResellerLink(data.id).then(async (resp) => {
      this.copyToClipboard(resp.url);
      await this.modal.success({
        nzTitle: 'Success!',
        nzContent: 'A one-time payment URL has been copied to your clipboard.'
      });
    })
    .catch((error) => {
      this.modal.error({
        nzTitle: 'Oops!',
        nzContent: error.data.message
      });
    });
    data.creating = false;
  }

  copyToClipboard(value: string) {
      const selBox = document.createElement('textarea');
      selBox.style.position = 'fixed';
      selBox.style.left = '0';
      selBox.style.top = '0';
      selBox.style.opacity = '0';
      selBox.value = value;
      document.body.appendChild(selBox);
      selBox.focus();
      selBox.select();
      document.execCommand('copy');
      document.body.removeChild(selBox);
  }
}
