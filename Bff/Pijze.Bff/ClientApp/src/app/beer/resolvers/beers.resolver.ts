import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { filter, first, tap } from 'rxjs';
import { SpinnerService } from 'src/app/core';
import { BeerActions, BeerState } from '../store';
import { getBeers } from '../store/selectors';

@Injectable({
  providedIn: 'root',
})
export class BeersResolver implements Resolve<any> {
  constructor(
    private spinner: SpinnerService,
    private store: Store<BeerState>
  ) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.spinner.show();
    return this.store.select(getBeers).pipe(
      tap((beers) => {
        if (!beers) {
          this.store.dispatch(BeerActions.loadBeers());
        }
      }),
      filter((beers) => !!beers),
      first(),
      tap((_) => this.spinner.hide())
    );
  }
}
