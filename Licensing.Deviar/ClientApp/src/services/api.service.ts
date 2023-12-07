import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class ApiService {
  constructor(private http: HttpClient) {

  }

  async createAccount(register) {
    var resp = await this.http.post<any>('api/register', register);

    return await resp.toPromise();
  }

  async unlockLicense(license) {
    var resp = await this.http.post<any>('api/key/unlock', license);

    return await resp.toPromise();
  }

  async login(login) {
    var resp = await this.http.post<any>('api/token', login);

    return await resp.toPromise();
  }

  async getUsageLogs(key) {
    var resp = await this.http.get<any>('api/key/usage/logs/' + key);

    return await resp.toPromise();
  }

  async getResellerSoftware() {
    var resp = await this.http.get<any>('api/reseller/list');

    return await resp.toPromise();
  }

  async deleteReseller(dto) {
    var resp = await this.http.post<any>('api/software/remove/reseller', dto);

    return await resp.toPromise();
  }

  async createResellerLink(softwareId) {
    var resp = await this.http.get<any>('api/reseller/start/' + softwareId);

    return await resp.toPromise();
  }

  async getResellers(softwareId) {
    var resp = await this.http.get<any>('api/software/resellers/' + softwareId);

    return await resp.toPromise();
  }

  async getSoftwareList() {
    var resp = await this.http.get<any>('api/software/list');

    return await resp.toPromise();
  }

  async editLicense(data) {
    var resp = await this.http.post<any>('api/key/edit', data);

    return await resp.toPromise();
  }

  async suspendLicense(license) {
    var resp = await this.http.post<any>('api/key/suspend', license);

    return await resp.toPromise();
  }

  async deleteLicense(license) {
    var resp = await this.http.post<any>('api/key/delete', license);

    return await resp.toPromise();
  }

  async addReseller(license) {
    var resp = await this.http.post<any>('api/software/reseller/add', license);

    return await resp.toPromise();
  }

  async saveSoftwareChanges(software) {
    var resp = await this.http.post<any>('api/software/edit', software);

    return await resp.toPromise();
  }

  async deleteSoftware(software) {
    var resp = await this.http.post<any>('api/software/delete', software);

    return await resp.toPromise();
  }

  async getSoftware(id) {
    var resp = await this.http.get<any>('api/software/get/' + id);

    return await resp.toPromise();
  }

  async addSoftware(software) {
    var resp = await this.http.post<any>('api/software/create', software);

    return await resp.toPromise();
  }

  async addLicense(software, license) {
    license.softwareId = software.id;
    var resp = await this.http.post<any>('api/key/create', license);

    return await resp.toPromise();
  }
}
