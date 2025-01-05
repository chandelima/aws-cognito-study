import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class BackendUrlService {

  private _baseUrl!: string;

  constructor() {
    this.setBaseUrl();
  }

  get baseUrl() {
    return this._baseUrl;
  }

  getUrl(route: string) {
    if (route.startsWith('/')) {
      route = route.substring(1);
    }

    return `${this._baseUrl}/${route}`;
  }

  private setBaseUrl() {
    this._baseUrl = environment.backendBaseUrl;
  }
}
