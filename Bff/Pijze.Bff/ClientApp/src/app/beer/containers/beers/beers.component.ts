import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { BeerFilters } from '../../models';
import { BeerActions, BeerState } from '../../store';
import { getBeers, isDataLoading } from '../../store/selectors';
import { getFilters } from '../../store/selectors/beer.selectors';
import { BeerListItem } from 'src/app/core';

@Component({
  selector: 'beers',
  templateUrl: './beers.component.html',
  styleUrls: ['./beers.component.scss'],
})
export class BeersComponent implements OnInit {
  beers$!: Observable<ReadonlyArray<BeerListItem>>;
  filters$!: Observable<BeerFilters | null>;
  isDataLoading$!: Observable<boolean>;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<BeerState>
  ) {}

  ngOnInit(): void {
    this.beers$ = this.store.select(getBeers);
    this.filters$ = this.store.select(getFilters);
    this.isDataLoading$ = this.store.select(isDataLoading);
  }

  protected onBeerClick = (id: string) =>
    this.router.navigate(['edit', id], { relativeTo: this.route });

  protected onBeersFilter = (filters: BeerFilters | null) =>
    this.store.dispatch(BeerActions.filterBeers({ filters }));
}
