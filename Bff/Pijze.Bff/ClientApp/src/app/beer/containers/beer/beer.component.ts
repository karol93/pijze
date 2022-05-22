import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Beer } from '../../models';
import { BeerService } from '../../services';

@Component({
  selector: 'beer',
  templateUrl: './beer.component.html',
  styleUrls: ['./beer.component.scss'],
})
export class BeerComponent implements OnInit {
  id?: string;
  beer?: Beer;

  constructor(
    private route: ActivatedRoute,
    private beerService: BeerService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data: any) => (this.beer = data.beer));
  }

  onBeerSave(beer: Beer): void {
    let save$: Observable<void> = beer.id
      ? this.beerService.update(beer)
      : this.beerService.create(beer);
    save$.subscribe((_) => this.router.navigate(['/']));
  }

  onBeerDelete(beer: Beer): void {
    this.beerService
      .delete(beer.id)
      .subscribe((_) => this.router.navigate(['/']));
  }
}
