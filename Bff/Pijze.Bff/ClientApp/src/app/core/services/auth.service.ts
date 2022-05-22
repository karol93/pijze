import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { UserInfo } from '../models';

const routes = {
  login: () => `/api/auth/login`,
  getUser: () => `api/auth/get-user`,
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isAuthenticated: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  public isAuthenticated$: Observable<boolean> =
    this._isAuthenticated.asObservable();

  constructor(private httpClient: HttpClient) {}

  public getUser(): Observable<UserInfo> {
    return this.httpClient.get<UserInfo>(routes.getUser()).pipe(
      tap((userInfo: UserInfo) => {
        if (userInfo.isAuthenticated) {
          this._isAuthenticated.next(true);
        }
      })
    );
  }

  public login(): void {
    window.location.href = routes.login();
  }

  public logout(): Observable<boolean> {
    this._isAuthenticated.next(false);
    return of(true);
  }
}
