import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import { BeerActions } from '../actions';
import { Router } from '@angular/router';
import { Beer, BeerService, BreweryService } from 'src/app/core';

@Injectable()
export class BeerEffects {
  constructor(
    private actions$: Actions,
    private beerService: BeerService,
    private breweryService: BreweryService,
    private router: Router
  ) {}

  loadBeers$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BeerActions.loadBeers),
      mergeMap(() =>
        this.beerService.getAll().pipe(
          map((beers) => BeerActions.loadBeersSuccess({ beers })),
          catchError((error) => of(BeerActions.loadBeersFail()))
        )
      )
    );
  });

  loadBrewery$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BeerActions.loadBeer),
      mergeMap((action) =>
        this.beerService.get(action.beerId).pipe(
          map((beer) => BeerActions.loadBeerSuccess({ beer })),
          catchError((error) => of(BeerActions.loadBeerFail()))
        )
      )
    );
  });

  deleteBrewery$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BeerActions.deleteBeer),
      mergeMap((action) =>
        this.beerService.delete(action.beerId).pipe(
          map(() => BeerActions.deleteBeerSuccess({ beerId: action.beerId })),
          catchError((error) => of(BeerActions.deleteBeerFail()))
        )
      )
    );
  });

  deleteBeerSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(BeerActions.deleteBeerSuccess),
        tap(({}) => {
          this.router.navigate(['/beer']);
        })
      ),
    { dispatch: false }
  );

  addBeer$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BeerActions.addBeer),
      mergeMap((action) =>
        this.beerService.create(action.beer).pipe(
          map((beer: Beer) => BeerActions.addBeerSuccess({ beer: beer })),
          catchError((error) => of(BeerActions.addBeerFail()))
        )
      )
    );
  });

  addBeerSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(BeerActions.addBeerSuccess),
        tap(({}) => {
          this.router.navigate(['/beer']);
        })
      ),
    { dispatch: false }
  );

  updateBeer$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BeerActions.updateBeer),
      mergeMap((action) =>
        this.beerService.update(action.beer).pipe(
          map(() => BeerActions.updateBeerSuccess({ beer: action.beer })),
          catchError((error) => of(BeerActions.updateBeerFail()))
        )
      )
    );
  });

  updateBeerSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(BeerActions.updateBeerSuccess),
        tap(({}) => {
          this.router.navigate(['/beer']);
        })
      ),
    { dispatch: false }
  );

  loadBreweries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(BeerActions.loadBreweries),
      mergeMap(() =>
        this.breweryService.getAll().pipe(
          map((breweries) => BeerActions.loadBreweriesSuccess({ breweries })),
          catchError((error) => of(BeerActions.loadBreweriesFail()))
        )
      )
    );
  });
}
