import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, catchError, filter, of, switchMap, take, tap } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { getBeersLoaded } from '../store/selectors';
import { BeerActions, BeerState } from '../store';

@Injectable({ providedIn: 'root' })
export class BeersLoadedGuard {
  constructor(private store: Store<BeerState>) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.checkBeersLoaded();
  }

  private checkBeersLoaded(): Observable<boolean> {
    return this.store.select(getBeersLoaded).pipe(
      tap((loaded) => {
        if (!loaded) {
          this.store.dispatch(BeerActions.loadBeers());
        }
      }),
      filter((loaded) => loaded),
      take(1),
      switchMap(() => of(true)),
      catchError(() => of(false))
    );
  }
}
