import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import { BeerService } from '../../services';
import { BeerActions } from '../actions';

@Injectable()
export class BeerEffects {
  constructor(private actions$: Actions, private beerService: BeerService) {}

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
}
