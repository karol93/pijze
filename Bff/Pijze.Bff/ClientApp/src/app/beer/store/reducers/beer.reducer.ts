import { createReducer, on } from '@ngrx/store';
import { BeerListItem } from '../../models';
import { BeerActions } from '../actions';

export interface BeerState {
  beers: ReadonlyArray<BeerListItem> | null;
  error: string;
}

const initialState: BeerState = {
  beers: null,
  error: '',
};

export const beerReducer = createReducer<BeerState>(
  initialState,
  on(BeerActions.loadBeers, (state) => ({
    ...state,
  })),
  on(BeerActions.loadBeersSuccess, (state, { beers }) => ({ ...state, beers }))
);
