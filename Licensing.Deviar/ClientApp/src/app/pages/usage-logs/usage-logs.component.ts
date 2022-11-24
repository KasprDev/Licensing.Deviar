import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/services/api.service';

@Component({
  selector: 'app-usage-logs',
  templateUrl: './usage-logs.component.html',
  styleUrls: ['./usage-logs.component.scss']
})
export class UsageLogsComponent implements OnInit {
  public logs: any[] = [];

  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    let key = this.route.snapshot.paramMap.get('key');
    await this.api.getUsageLogs(key).then((resp) => {
      this.logs = resp;
    });
  }
}
