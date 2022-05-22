import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BeerFilters, BeerListItem } from '../../models';

@Component({
  selector: 'beers',
  templateUrl: './beers.component.html',
  styleUrls: ['./beers.component.scss'],
})
export class BeersComponent implements OnInit {
  beerList: Array<BeerListItem> = [];
  beerListFiltered: Array<BeerListItem> = [];

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.data.subscribe(
      (data: any) => (this.beerList = this.beerListFiltered = data.beers)
    );
  }

  onBeerClick(id: string): void {
    this.router.navigate(['edit', id], { relativeTo: this.route });
  }

  onBeersFilter(filters: BeerFilters | null): void {
    if (filters) {
      const lowerText = filters.text.toLocaleLowerCase();
      this.beerListFiltered = this.beerList.filter(
        (beer) =>
          (lowerText && lowerText.length
            ? beer.name.toLocaleLowerCase().startsWith(lowerText) ||
              beer.manufacturer.toLocaleLowerCase().startsWith(lowerText)
            : true) && (filters.rating ? beer.rating == filters.rating : true)
      );
    } else this.beerListFiltered = this.beerList;
  }
}
