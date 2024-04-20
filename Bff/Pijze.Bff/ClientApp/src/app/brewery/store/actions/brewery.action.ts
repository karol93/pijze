import { createAction, props } from '@ngrx/store';
import { Brewery } from 'src/app/core';

export const loadBreweries = createAction('[Brewery] Load Breweries');
export const loadBreweriesSuccess = createAction(
  '[Brewery] Load Breweries Success',
  props<{ breweries: ReadonlyArray<Brewery> }>()
);
export const loadBreweriesFail = createAction('[Brewery] Load Breweries Fail');

export const loadBrewery = createAction(
  '[Brewery] Load Brewery',
  props<{ breweryId: string }>()
);

export const loadBrewerySuccess = createAction(
  '[Brewery] Load Brewery Success',
  props<{ brewery: Brewery }>()
);

export const loadBreweryFail = createAction('[Brewery] Load Brewery Fail');

export const deleteBrewery = createAction(
  '[Brewery] Delete Brewery',
  props<{ breweryId: string }>()
);

export const deleteBrewerySuccess = createAction(
  '[Brewery] Delete Brewery Success',
  props<{ breweryId: string }>()
);

export const deleteBreweryFail = createAction('[Brewery] Delete Brewery Fail');

export const addBrewery = createAction(
  '[Brewery] Add Brewery',
  props<{ brewery: Brewery }>()
);

export const addBrewerySuccess = createAction(
  '[Brewery] Add Brewery Success',
  props<{ brewery: Brewery }>()
);

export const addBreweryFail = createAction('[Brewery] Add Brewery Fail');

export const updateBrewery = createAction(
  '[Brewery] Update Brewery',
  props<{ brewery: Brewery }>()
);

export const updateBrewerySuccess = createAction(
  '[Brewery] Update Brewery Success',
  props<{ brewery: Brewery }>()
);

export const updateBreweryFail = createAction('[Brewery] Update Brewery Fail');

export const changePage = createAction(
  '[Brewery] Change Page',
  props<{ pageNumber: number }>()
);

export const resetSelectedBrewery = createAction(
  '[Brewery] Reset Selected Brewery'
);
