import { createFeature, createReducer, on } from '@ngrx/store';
import { BeerFilters } from '../../models';
import { BeerActions } from '../actions';
import { Beer, BeerListItem, Brewery } from 'src/app/core';

export interface BeerState {
  beers: ReadonlyArray<BeerListItem>;
  beersLoaded: boolean;
  filters: BeerFilters | null;
  isDataLoading: boolean;
  error: string;
  selected: Beer | null;
  breweries: ReadonlyArray<Brewery>;
  breweriesLoaded: boolean;
}

const initialState: BeerState = {
  beers: [],
  beersLoaded: false,
  filters: null,
  isDataLoading: false,
  error: '',
  selected: null,
  breweries: [],
  breweriesLoaded: false,
};

export const beerReducer = createFeature({
  name: 'beer',
  reducer: createReducer(
    initialState,
    on(BeerActions.loadBeers, (state) => ({
      ...state,
      isDataLoading: true,
      beersLoaded: false,
    })),
    on(BeerActions.loadBeersSuccess, (state, { beers }) => ({
      ...state,
      beers,
      isDataLoading: false,
      beersLoaded: true,
    })),
    on(BeerActions.loadBeer, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BeerActions.loadBeerSuccess, (state, { beer }) => ({
      ...state,
      isDataLoading: false,
      selected: beer,
    })),
    on(BeerActions.filterBeers, (state, { filters }) => ({
      ...state,
      filters,
    })),
    on(BeerActions.addBeer, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BeerActions.addBeerSuccess, (state, { beer }) => ({
      ...state,
      beers: [
        {
          id: beer.id!,
          brewery: 'dipa',
          name: beer.name,
          rating: beer.rating,
        },
        ...state.beers,
      ],
      isDataLoading: false,
    })),
    on(BeerActions.updateBeer, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BeerActions.updateBeerSuccess, (state, { beer }) => {
      const beerIndex = state.beers.findIndex((b) => b.id === beer.id);
      if (beerIndex !== -1) {
        const updatedBeers = [...state.beers];
        updatedBeers[beerIndex] = {
          id: beer.id!,
          brewery: beer.brewery!,
          name: beer.name,
          rating: beer.rating,
        };

        return {
          ...state,
          beers: updatedBeers,
          selected: beer,
          isDataLoading: false,
        };
      }

      return state;
    }),
    on(BeerActions.deleteBeer, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BeerActions.deleteBeerSuccess, (state, { beerId }) => {
      const updatedBeers = state.beers.filter((b) => b.id !== beerId);

      return {
        ...state,
        isDataLoading: false,
        beers: updatedBeers,
      };
    }),
    on(BeerActions.resetSelectedBeer, (state) => ({
      ...state,
      selected: null,
    })),
    on(BeerActions.loadBreweries, (state) => ({
      ...state,
      isDataLoading: true,
      breweriesLoaded: false,
    })),
    on(BeerActions.loadBreweriesSuccess, (state, { breweries }) => ({
      ...state,
      breweries,
      isDataLoading: false,
      breweriesLoaded: true,
    }))
  ),
});
