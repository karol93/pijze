import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BeerActions, BeerState } from '../../store';
import { Store } from '@ngrx/store';
import {
  getBreweries,
  getSelected,
  isDataLoading,
} from '../../store/selectors';
import { Beer, Brewery } from 'src/app/core';

@Component({
  selector: 'beer',
  templateUrl: './beer.component.html',
})
export class BeerComponent {
  beer$: Observable<Beer | null>;
  breweries$: Observable<ReadonlyArray<Brewery>>;
  isDataLoading$: Observable<boolean>;

  constructor(private store: Store<BeerState>) {
    this.isDataLoading$ = this.store.select(isDataLoading);
    this.beer$ = this.store.select(getSelected);
    this.breweries$ = this.store.select(getBreweries);
  }

  protected onBeerSave(beer: Beer): void {
    beer.id
      ? this.store.dispatch(BeerActions.updateBeer({ beer }))
      : this.store.dispatch(BeerActions.addBeer({ beer }));
  }

  protected onBeerDelete(beer: Beer): void {
    this.store.dispatch(BeerActions.deleteBeer({ beerId: beer.id! }));
  }
}
