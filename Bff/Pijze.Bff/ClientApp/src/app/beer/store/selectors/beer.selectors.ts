import { state } from '@angular/animations';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BeerFilters } from '../../models';
import { BeerState } from '../reducers';

export const getBeerState = createFeatureSelector<BeerState>('beer');

export const getFilters = createSelector(
  getBeerState,
  (state: BeerState) => state.filters
);

export const getBeersLoaded = createSelector(
  getBeerState,
  (state: BeerState) => state.beersLoaded
);

export const isDataLoading = createSelector(
  getBeerState,
  (state: BeerState) => state.isDataLoading
);

export const getBeers = createSelector(
  getBeerState,
  getFilters,
  (state: BeerState, filters: BeerFilters | null) => {
    if (!filters) return state.beers;
    if (!state.beers) return [];
    const lowerText = filters.text.toLocaleLowerCase();
    return state.beers.filter(
      (beer) =>
        (lowerText && lowerText.length
          ? beer.name.toLocaleLowerCase().startsWith(lowerText) ||
            beer.brewery.toLocaleLowerCase().startsWith(lowerText)
          : true) && (filters.rating ? beer.rating == filters.rating : true)
    );
  }
);

export const getSelected = createSelector(
  getBeerState,
  (state: BeerState) => state.selected
);

export const getBreweriesLoaded = createSelector(
  getBeerState,
  (state: BeerState) => state.breweriesLoaded
);

export const getBreweries = createSelector(getBeerState, (state: BeerState) => {
  return state.breweries;
});
