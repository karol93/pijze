import { createAction, props } from '@ngrx/store';
import { BeerListItem } from '../../models';

export const loadBeers = createAction('[Beer] Load Beers');
export const loadBeersSuccess = createAction(
  '[Beer] Load Beers Success',
  props<{ beers: ReadonlyArray<BeerListItem> }>()
);
export const loadBeersFail = createAction('[Beer] Load Beers Fail');
