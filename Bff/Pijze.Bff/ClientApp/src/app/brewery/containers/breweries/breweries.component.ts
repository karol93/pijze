import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import {
  BreweryActions,
  BreweryState,
  getPaginatedBreweries,
  getPagination,
  isDataLoading,
} from '../../store';
import { Brewery, Pagination } from 'src/app/core';

@Component({
  selector: 'breweries',
  templateUrl: './breweries.component.html',
})
export class BreweriesComponent implements OnInit {
  breweries$: Observable<ReadonlyArray<Brewery>>;
  isDataLoading$: Observable<boolean>;
  pagination$: Observable<Pagination>;

  constructor(private store: Store<BreweryState>) {
    this.breweries$ = this.store.select(getPaginatedBreweries);
    this.isDataLoading$ = this.store.select(isDataLoading);
    this.pagination$ = this.store.select(getPagination);
  }

  ngOnInit(): void {}

  protected onBreweryDeletActionClick = (id: string) =>
    this.store.dispatch(BreweryActions.deleteBrewery({ breweryId: id }));

  protected onPageChange = (pageNumber: number) =>
    this.store.dispatch(BreweryActions.changePage({ pageNumber }));
}
