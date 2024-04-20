import { Injectable } from '@angular/core';
import { BreweryActions, BreweryState, getSelected } from '../store';
import { Store } from '@ngrx/store';
import { Observable, catchError, filter, of, switchMap, take, tap } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class BreweryExistsGuard {
  constructor(private store: Store<BreweryState>) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    const breweryId = route.params['id'];
    if (breweryId) {
      return this.checkBreweryExists(breweryId);
    } else {
      this.store.dispatch(BreweryActions.resetSelectedBrewery());
      return of(true);
    }
  }

  private checkBreweryExists(breweryId: string): Observable<boolean> {
    return this.store.select(getSelected).pipe(
      tap((selectedBrewery) => {
        if (!selectedBrewery || selectedBrewery.id !== breweryId) {
          this.store.dispatch(BreweryActions.loadBrewery({ breweryId }));
        }
      }),
      filter((selectedBrewery) => selectedBrewery?.id === breweryId),
      take(1),
      switchMap(() => of(true)),
      catchError(() => of(false))
    );
  }
}
