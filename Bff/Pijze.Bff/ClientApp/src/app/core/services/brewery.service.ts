import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brewery } from '../models';

const routes = {
  getAll: () => `/api/brewery`,
  get: (id: string) => `/api/brewery/${id}`,
  create: () => `/api/brewery/`,
  update: (id: string) => `/api/brewery/${id}`,
  delete: (id: string) => `/api/brewery/${id}`,
};

@Injectable({
  providedIn: 'root',
})
export class BreweryService {
  constructor(private httpClient: HttpClient) {}

  public get(id: string): Observable<Brewery> {
    return this.httpClient.get<Brewery>(routes.get(id));
  }

  public getAll(): Observable<Array<Brewery>> {
    return this.httpClient.get<Array<Brewery>>(routes.getAll());
  }

  public create(brewery: Brewery): Observable<Brewery> {
    return this.httpClient.post<Brewery>(routes.create(), brewery);
  }

  public update(brewery: Brewery): Observable<void> {
    return this.httpClient.post<void>(routes.update(brewery.id!), brewery);
  }

  public delete(id: string): Observable<void> {
    return this.httpClient.delete<void>(routes.delete(id));
  }
}
