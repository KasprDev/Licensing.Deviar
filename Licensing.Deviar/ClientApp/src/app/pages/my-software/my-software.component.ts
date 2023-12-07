import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  public reseller: any = {};

  public addingReseller: boolean = false;

  public adding: boolean;

  constructor(private api: ApiService, private modal: NzModalService, private router: Router) { }

  async ngOnInit() {
    if (localStorage.getItem('reseller'))
      this.router.navigate(['/resell']);
    
    this.software = await this.api.getSoftwareList();
  }

  selectSoftware(s) {
    this.selectedSoftware = s;
    this.createLicense = true;
  }

  addReseller(data) {
    this.selectedSoftware = data;
    this.addingReseller = true;
  }

  async createReseller() {
    this.adding = true;

    this.reseller.softwareId = this.selectedSoftware.id;

    await this.api.addReseller(this.reseller).then((resp) => {
      this.modal.success({
        nzTitle: 'Success!',
        nzContent: `${this.reseller.name} has been added as a reseller!`
      });
      this.addingReseller = false;
      this.reseller = {};
    })
    .catch(async (error) => {
      await this.modal.create({
        nzTitle: 'Oops!',
        nzContent: error.data.message
      });
    });
    this.adding = false;
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

  async deleteSoftware(software) {
    await this.modal.confirm({
      nzTitle: 'Delete Software',
      nzContent: 'Are you sure you want to delete this software? This cannot be undone.',
      nzOkDanger: true,
      nzOkText: 'Delete',
      nzOnOk: async () => {
        await this.api.deleteSoftware(software).then((resp) => {
          this.software.splice(this.software.indexOf(software), 1);
        }).catch((error) => {
          this.modal.error({
            nzTitle: 'Oops!',
            nzContent: error.data.message
          });
        });
      }
    });
  }

  async addLicense() {
    this.adding = true;

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

    this.adding = false;
  }
}
