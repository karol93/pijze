import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './core';

@Injectable({
  providedIn: 'root',
})
export class AppInitializerService {
  constructor(private authService: AuthService) {}

  public init(): Observable<any> {
    return this.authService.getUser();
  }
}
