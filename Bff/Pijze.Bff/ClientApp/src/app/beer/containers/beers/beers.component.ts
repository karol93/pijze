import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { BeerFilters, BeerListItem } from '../../models';
import { BeerActions, BeerState } from '../../store';
import { getBeers, isDataLoading } from '../../store/selectors';

@Component({
  selector: 'beers',
  templateUrl: './beers.component.html',
  styleUrls: ['./beers.component.scss'],
})
export class BeersComponent implements OnInit {
  beers$!: Observable<ReadonlyArray<BeerListItem>>;
  isDataLoading$!: Observable<boolean>;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<BeerState>
  ) {}

  ngOnInit(): void {
    this.store.dispatch(BeerActions.loadBeers());
    this.beers$ = this.store.select(getBeers);
    this.isDataLoading$ = this.store.select(isDataLoading);
  }

  onBeerClick = (id: string) =>
    this.router.navigate(['edit', id], { relativeTo: this.route });

  onBeersFilter = (filters: BeerFilters | null) =>
    this.store.dispatch(BeerActions.filterBeers({ filters }));
}
