import { createFeature, createReducer, on } from '@ngrx/store';
import { BreweryActions } from '../actions';
import { Brewery, Pagination } from 'src/app/core';

export interface BreweryState {
  breweries: ReadonlyArray<Brewery>;
  breweriesLoaded: boolean;
  selected: Brewery | null;
  isDataLoading: boolean;
  error: string;
  pagination: Pagination;
}

const initialState: BreweryState = {
  breweries: [],
  breweriesLoaded: false,
  selected: null,
  isDataLoading: false,
  error: '',
  pagination: {
    currentPage: 1,
    itemsPerPage: 15,
    totalItems: 0,
  },
};

export const breweryReducer = createFeature({
  name: 'brewery',
  reducer: createReducer(
    initialState,
    on(BreweryActions.loadBreweries, (state) => ({
      ...state,
      isDataLoading: true,
      breweriesLoaded: false,
    })),
    on(BreweryActions.loadBreweriesSuccess, (state, { breweries }) => ({
      ...state,
      breweries,
      isDataLoading: false,
      breweriesLoaded: true,
      pagination: {
        ...state.pagination,
        totalItems: breweries.length,
      },
    })),
    on(BreweryActions.loadBrewery, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BreweryActions.loadBrewerySuccess, (state, { brewery }) => ({
      ...state,
      isDataLoading: false,
      selected: brewery,
    })),
    on(BreweryActions.deleteBrewery, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BreweryActions.deleteBrewerySuccess, (state, { breweryId }) => {
      const updatedBreweries = state.breweries.filter(
        (b) => b.id !== breweryId
      );
      const newTotalItems = updatedBreweries.length;
      const newCurrentPage =
        state.pagination.currentPage > 1 &&
        state.pagination.itemsPerPage * (state.pagination.currentPage - 1) >=
          newTotalItems
          ? state.pagination.currentPage - 1
          : state.pagination.currentPage;

      return {
        ...state,
        isDataLoading: false,
        breweries: updatedBreweries,
        pagination: {
          ...state.pagination,
          currentPage: newCurrentPage,
          totalItems: updatedBreweries.length,
        },
      };
    }),
    on(BreweryActions.changePage, (state, { pageNumber }) => ({
      ...state,
      pagination: {
        ...state.pagination,
        currentPage: pageNumber,
      },
    })),
    on(BreweryActions.addBrewery, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BreweryActions.addBrewerySuccess, (state, { brewery }) => ({
      ...state,
      breweries: [...state.breweries, brewery],
      isDataLoading: false,
    })),
    on(BreweryActions.updateBrewery, (state) => ({
      ...state,
      isDataLoading: true,
    })),
    on(BreweryActions.updateBrewerySuccess, (state, { brewery }) => {
      const breweryIndex = state.breweries.findIndex(
        (b) => b.id === brewery.id
      );
      if (breweryIndex !== -1) {
        const updatedBreweries = [...state.breweries];
        updatedBreweries[breweryIndex] = brewery;

        return {
          ...state,
          breweries: updatedBreweries,
          isDataLoading: false,
        };
      }

      return state;
    }),
    on(BreweryActions.resetSelectedBrewery, (state) => ({
      ...state,
      selected: null,
    }))
  ),
});
