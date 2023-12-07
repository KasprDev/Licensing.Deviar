import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-reseller-list',
  templateUrl: `./reseller-list.component.html`,
  styleUrl: './reseller-list.component.css',
})

export class ResellerListComponent { 
  public resellers: any[] = [];

  constructor(private api: ApiService, private route: ActivatedRoute, private modal: NzModalService) {}

  async ngOnInit() {
    const id = this.route.snapshot.params['id'];
    this.resellers = await this.api.getResellers(id);
  }

  async deleteReseller(dto) {
    await this.modal.confirm({
      nzTitle: 'Remove Reseller',
      nzContent: 'Are you sure you want to delete this reseller?',
      nzOkDanger: true,
      nzOkText: 'Delete',
      nzOnOk: async () => {
        await this.api.deleteReseller(dto).then((resp) => {
          this.modal.success({
            nzTitle: 'Success!',
            nzContent: 'Reseller deleted successfully.'
          });
        })
        .catch((error) => {
          this.modal.error({
            nzTitle: 'Oops!',
            nzContent: error.data.message
          });
        });
      }
    });
  }
}
