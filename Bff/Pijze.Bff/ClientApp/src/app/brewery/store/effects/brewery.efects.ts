import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { BreweryActions } from '../actions';
import { Router } from '@angular/router';
import { Brewery, BreweryService } from 'src/app/core';

@Injectable()
export class BreweryEffects {
  constructor(
    private actions$: Actions,
    private breweryService: BreweryService,
    private router: Router
  ) {}

  loadBreweries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BreweryActions.loadBreweries),
      mergeMap(() =>
        this.breweryService.getAll().pipe(
          map((breweries) =>
            BreweryActions.loadBreweriesSuccess({ breweries })
          ),
          catchError((error) => of(BreweryActions.loadBreweriesFail()))
        )
      )
    );
  });

  loadBrewery$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BreweryActions.loadBrewery),
      mergeMap((action) =>
        this.breweryService.get(action.breweryId).pipe(
          map((brewery) => BreweryActions.loadBrewerySuccess({ brewery })),
          catchError((error) => of(BreweryActions.loadBreweryFail()))
        )
      )
    );
  });

  deleteBrewery$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BreweryActions.deleteBrewery),
      mergeMap((action) =>
        this.breweryService.delete(action.breweryId).pipe(
          map(() =>
            BreweryActions.deleteBrewerySuccess({ breweryId: action.breweryId })
          ),
          catchError((error) => of(BreweryActions.deleteBreweryFail()))
        )
      )
    );
  });

  addBrewery$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BreweryActions.addBrewery),
      mergeMap((action) =>
        this.breweryService.create(action.brewery).pipe(
          map((brewery: Brewery) =>
            BreweryActions.addBrewerySuccess({ brewery: brewery })
          ),
          catchError((error) => of(BreweryActions.addBreweryFail()))
        )
      )
    );
  });

  addBrewerySuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(BreweryActions.addBrewerySuccess),
        tap(({}) => {
          this.router.navigate(['/brewery']);
        })
      ),
    { dispatch: false }
  );

  updateBrewery$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BreweryActions.updateBrewery),
      mergeMap((action) =>
        this.breweryService.update(action.brewery).pipe(
          map(() =>
            BreweryActions.updateBrewerySuccess({ brewery: action.brewery })
          ),
          catchError((error) => of(BreweryActions.updateBreweryFail()))
        )
      )
    );
  });

  updateBrewerySuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(BreweryActions.updateBrewerySuccess),
        tap(({}) => {
          this.router.navigate(['/brewery']);
        })
      ),
    { dispatch: false }
  );
}
