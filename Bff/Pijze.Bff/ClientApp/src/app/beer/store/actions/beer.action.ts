import { createAction, props } from '@ngrx/store';
import { BeerFilters, BeerListItem } from '../../models';

export const loadBeers = createAction('[Beer] Load Beers');
export const loadBeersSuccess = createAction(
  '[Beer] Load Beers Success',
  props<{ beers: ReadonlyArray<BeerListItem> }>()
);
export const loadBeersFail = createAction('[Beer] Load Beers Fail');

export const filterBeers = createAction(
  '[Beer] Filter Beers',
  props<{ filters: BeerFilters | null }>()
);
