import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, catchError, filter, of, switchMap, take, tap } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BeerActions, BeerState } from '../store';
import { getSelected } from '../store/selectors';

@Injectable({ providedIn: 'root' })
export class BeerExistsGuard {
  constructor(private store: Store<BeerState>) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    const beerId = route.params['id'];
    if (beerId) {
      return this.checkBeerExists(beerId);
    } else {
      this.store.dispatch(BeerActions.resetSelectedBeer());
      return of(true);
    }
  }

  private checkBeerExists(beerId: string): Observable<boolean> {
    return this.store.select(getSelected).pipe(
      tap((selectedBeer) => {
        if (!selectedBeer || selectedBeer.id !== beerId) {
          this.store.dispatch(BeerActions.loadBeer({ beerId }));
        }
      }),
      filter((selectedBeer) => selectedBeer?.id === beerId),
      take(1),
      switchMap(() => of(true)),
      catchError(() => of(false))
    );
  }
}
