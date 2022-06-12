import { createFeatureSelector, createSelector } from '@ngrx/store';
import { BeerState } from '../reducers';

export const getBeerState = createFeatureSelector<BeerState>('beer');

export const getBeers = createSelector(
  getBeerState,
  (state: BeerState) => state.beers
);
