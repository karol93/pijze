import { Component, OnInit } from '@angular/core';
import {
  BreweryActions,
  BreweryState,
  getSelected,
  isDataLoading,
} from '../../store';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Brewery } from 'src/app/core';

@Component({
  selector: 'brewery',
  templateUrl: './brewery.component.html',
})
export class BreweryComponent {
  brewery$: Observable<Brewery | null>;
  isDataLoading$: Observable<boolean>;

  constructor(private store: Store<BreweryState>) {
    this.isDataLoading$ = this.store.select(isDataLoading);
    this.brewery$ = this.store.select(getSelected);
  }

  protected onBrewerySave(brewery: Brewery): void {
    brewery.id
      ? this.store.dispatch(BreweryActions.updateBrewery({ brewery }))
      : this.store.dispatch(BreweryActions.addBrewery({ brewery }));
    // let save$: Observable<void> = brewery.id
    //   ? this.breweryService.update(brewery)
    //   : this.breweryService.create(brewery);
    // save$.subscribe((_) => this.router.navigate(['/brewery']));
  }
}
