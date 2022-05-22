import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Beer, BeerListItem } from '../models';

const routes = {
  getAll: () => `/api/beer`,
  get: (id: string) => `/api/beer/${id}`,
  create: () => `/api/beer/`,
  update: (id: string) => `/api/beer/${id}`,
  delete: (id: string) => `/api/beer/${id}`,
};

@Injectable({
  providedIn: 'root',
})
export class BeerService {
  constructor(private httpClient: HttpClient) {}

  public get(id: string): Observable<Beer> {
    return this.httpClient.get<Beer>(routes.get(id));
  }

  public getAll(): Observable<Array<BeerListItem>> {
    return this.httpClient.get<Array<BeerListItem>>(routes.getAll());
  }

  public create(beer: Beer): Observable<void> {
    return this.httpClient.post<void>(routes.create(), beer);
  }

  public update(beer: Beer): Observable<void> {
    return this.httpClient.post<void>(routes.update(beer.id), beer);
  }

  public delete(id: string): Observable<void> {
    return this.httpClient.delete<void>(routes.delete(id));
  }
}
