import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BackendUrlService } from '../../core/services/backend-url.service';
import { IDefaultResponse } from '../../core/interfaces/default-response.interface';
import { IReadToDo } from '../../interfaces/to-do/read-to-do.interface';
import { ICreateToDo } from '../../interfaces/to-do/create-to-do.interface';
import { IUpdateTodo } from '../../interfaces/to-do/update-to-do.interface';

@Injectable({
  providedIn: 'root'
})
export class ToDoService {

  private _baseUrl!: string;

  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _backendUrlService: BackendUrlService
  ) {
    this.setBaseUrl();
  }

  private setBaseUrl() {
    this._baseUrl = this._backendUrlService.getUrl('/v1/todos');
  }

  public getAll(): Observable<IDefaultResponse<IReadToDo[]>> {
    return this._httpClient.get<IDefaultResponse<IReadToDo[]>>(this._baseUrl);
  }

  public create(data: ICreateToDo): Observable<IDefaultResponse> {
    return this._httpClient.post<IDefaultResponse>(this._baseUrl, data);
  }

  public update(data: IUpdateTodo): Observable<IDefaultResponse> {
    return this._httpClient.put<IDefaultResponse>(this._baseUrl, data);
  }

  public delete(id: string): Observable<IDefaultResponse> {
    const url = `${this._baseUrl}/${id}`;
    return this._httpClient.delete<IDefaultResponse>(url);
  }
}
