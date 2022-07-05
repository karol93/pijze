import { createFeature, createReducer, on } from '@ngrx/store';
import { BeerFilters, BeerListItem } from '../../models';
import { BeerActions } from '../actions';

export interface BeerState {
  beers: ReadonlyArray<BeerListItem>;
  filters: BeerFilters | null;
  isDataLoading: boolean;
  error: string;
}

const initialState: BeerState = {
  beers: [],
  filters: null,
  isDataLoading: false,
  error: '',
};

export const beerReducer = createFeature({
  name: 'beer',
  reducer: createReducer(
    initialState,
    on(BeerActions.loadBeers, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BeerActions.loadBeersSuccess, (state, { beers }) => ({
      ...state,
      beers,
      isDataLoading: false,
    })),
    on(BeerActions.filterBeers, (state, { filters }) => ({
      ...state,
      filters,
    }))
  ),
});
