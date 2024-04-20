import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, catchError, filter, of, switchMap, take, tap } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { getBreweriesLoaded } from '../store/selectors';
import { BeerActions, BeerState } from '../store';

@Injectable({ providedIn: 'root' })
export class BreweriesLoadedGuard {
  constructor(private store: Store<BeerState>) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.checkBreweriesLoaded();
  }

  private checkBreweriesLoaded(): Observable<boolean> {
    return this.store.select(getBreweriesLoaded).pipe(
      tap((loaded) => {
        if (!loaded) {
          this.store.dispatch(BeerActions.loadBreweries());
        }
      }),
      filter((loaded) => loaded),
      take(1),
      switchMap(() => of(true)),
      catchError(() => of(false))
    );
  }
}
