import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { tap } from 'rxjs';
import { SpinnerService } from 'src/app/core';
import { BeerService } from '../services';

@Injectable({
  providedIn: 'root',
})
export class BeersResolver implements Resolve<any> {
  constructor(
    private beerService: BeerService,
    private spinner: SpinnerService
  ) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.spinner.show();
    return this.beerService.getAll().pipe(tap((_) => this.spinner.hide()));
  }
}
