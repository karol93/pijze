import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BreweryState } from '../reducers/brewery.reducer';

export const getBreweryState = createFeatureSelector<BreweryState>('brewery');

export const isDataLoading = createSelector(
  getBreweryState,
  (state: BreweryState) => state.isDataLoading
);

export const getBreweriesLoaded = createSelector(
  getBreweryState,
  (state: BreweryState) => state.breweriesLoaded
);

export const getBreweries = createSelector(
  getBreweryState,
  (state: BreweryState) => {
    return state.breweries;
  }
);

export const getSelected = createSelector(
  getBreweryState,
  (state: BreweryState) => state.selected
);

export const getPagination = createSelector(
  getBreweryState,
  (state: BreweryState) => {
    return state.pagination;
  }
);

export const getPaginatedBreweries = createSelector(
  getBreweries,
  getPagination,
  (breweryList, pagination) => {
    const startIndex = (pagination.currentPage - 1) * pagination.itemsPerPage;
    const endIndex = startIndex + pagination.itemsPerPage;
    return breweryList.slice(startIndex, endIndex);
  }
);
