import { Component, OnInit } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-my-software',
  templateUrl: './my-software.component.html',
  styleUrls: ['./my-software.component.scss']
})
export class MySoftwareComponent implements OnInit {
  public createSoftware: boolean = false;
  public createLicense: boolean = false;
  public selectedSoftware: any = {};

  public newSoftware: any = {};
  public newLicense: any = {};
  public software: any[] = [];

  constructor(private api: ApiService, private modal: NzModalService) { }

  async ngOnInit() {
    this.software = await this.api.getSoftwareList();
  }

  selectSoftware(s) {
    this.selectedSoftware = s;
    this.createLicense = true;
  }

  async addSoftware() {
    await this.api.addSoftware(this.newSoftware).then((resp) => {
      window.location.reload();
    })
    .catch(async (error) => {
      await this.modal.create({
        nzTitle: 'Oops!',
        nzContent: error.data.message
      });
    });
  }

  async addLicense() {
    await this.api.addLicense(this.selectedSoftware, this.newLicense).then(async (resp) => {
      this.createLicense = false;
      this.selectedSoftware.licenses++;
      await this.modal.create({
        nzTitle: 'New Software License Key',
        nzContent: resp.message
      });
    })
    .catch(async (error) => {
      console.log(error);
      await this.modal.create({
        nzTitle: 'Oops!',
        nzContent: error.data.message
      });
    });
  }
}
