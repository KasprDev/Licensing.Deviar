import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-view-software',
  templateUrl: './view-software.component.html',
  styleUrls: ['./view-software.component.scss']
})
export class ViewSoftwareComponent implements OnInit {
  public software: any = {};
  public selectedLicense: any = {};
  public editingLicense: boolean = false;
  public editingSoftware: boolean = false;

  public saving: boolean = false;

  constructor(private route: ActivatedRoute, private api: ApiService, private modal: NzModalService) { }

  async ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');

    this.software = await this.api.getSoftware(id);
  }

  async editSoftware() {
    this.saving = true;

    await this.api.saveSoftwareChanges(this.software).then(async (resp) => {
      this.editingSoftware = false;
      await this.modal.create({
        nzTitle: 'Success!',
        nzContent: `${this.software.name} has been updated successfully!`
      });
    })
    .catch(async (error) => {
      await this.modal.create({
        nzTitle: 'Oops!',
        nzContent: error.data.message
      });
    });

    this.saving = false;
  }

  async unlockKey(data) {
    await this.api.unlockLicense(data).then((resp) => {
      data.hardwareId = null;
    });
  }

  async editLicense() {
    this.saving = true;
    await this.api.editLicense(this.selectedLicense).then((resp) => {
      this.editingLicense = false;
    });
    this.saving = false;
  }

  async suspendLicense(license) {
    if (!license.locked) {
      await this.modal.create({
        nzTitle: 'Suspend License',
        nzContent: 'Are you sure you want to suspend this license?',
        nzOnOk: async () => {
          await this.api.suspendLicense(license).then((resp) => {
            license.locked = !license.locked;
          });
        }
      });
    } else {
      await this.modal.create({
        nzTitle: 'Reactivate License',
        nzContent: 'Are you sure you want to reactivate this license?',
        nzOnOk: async () => {
          await this.api.suspendLicense(license).then((resp) => {
            license.locked = !license.locked;
          });
        }
      });
    }
  }

  changeExpireDate(data) {
    this.selectedLicense = data;
    this.editingLicense = true;
  }

  async deleteLicense(license) {
    await this.modal.create({
      nzTitle: 'Delete License',
      nzContent: 'Are you sure you want to delete this license?',
      nzOkDanger: true,
      nzOnOk: async () => {
        await this.api.deleteLicense(license).then((resp) => {
          window.location.reload();
        });
      }
    });
  }
}
