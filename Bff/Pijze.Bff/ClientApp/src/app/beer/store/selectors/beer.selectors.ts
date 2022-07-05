import { state } from '@angular/animations';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BeerFilters } from '../../models';
import { BeerState } from '../reducers';

export const getBeerState = createFeatureSelector<BeerState>('beer');

export const getFilters = createSelector(
  getBeerState,
  (state: BeerState) => state.filters
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
            beer.manufacturer.toLocaleLowerCase().startsWith(lowerText)
          : true) && (filters.rating ? beer.rating == filters.rating : true)
    );
  }
);
