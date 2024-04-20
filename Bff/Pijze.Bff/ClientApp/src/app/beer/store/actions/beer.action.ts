import { createAction, props } from '@ngrx/store';
import { BeerFilters } from '../../models';
import { Beer, BeerListItem, Brewery } from 'src/app/core';

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

export const loadBeer = createAction(
  '[Beer] Load Beer',
  props<{ beerId: string }>()
);

export const loadBeerSuccess = createAction(
  '[Beer] Load Beer Success',
  props<{ beer: Beer }>()
);

export const loadBeerFail = createAction('[Beer] Load Beer Fail');

export const deleteBeer = createAction(
  '[Beer] Delete Beer',
  props<{ beerId: string }>()
);

export const deleteBeerSuccess = createAction(
  '[Beer] Delete Beer Success',
  props<{ beerId: string }>()
);

export const deleteBeerFail = createAction('[Beer] Delete Beer Fail');

export const addBeer = createAction('[Beer] Add Beer', props<{ beer: Beer }>());

export const addBeerSuccess = createAction(
  '[Beer] Add Beer Success',
  props<{ beer: Beer }>()
);

export const addBeerFail = createAction('[Beer] Add Beer Fail');

export const updateBeer = createAction(
  '[Beer] Update Beer',
  props<{ beer: Beer }>()
);

export const updateBeerSuccess = createAction(
  '[Beer] Update Beer Success',
  props<{ beer: Beer }>()
);

export const updateBeerFail = createAction('[Beer] Update Beer Fail');

export const resetSelectedBeer = createAction('[Beer] Reset Selected Beer');

export const loadBreweries = createAction('[Beer] Load Breweries');
export const loadBreweriesSuccess = createAction(
  '[Beer] Load Breweries Success',
  props<{ breweries: ReadonlyArray<Brewery> }>()
);
export const loadBreweriesFail = createAction('[Beer] Load Breweries Fail');
