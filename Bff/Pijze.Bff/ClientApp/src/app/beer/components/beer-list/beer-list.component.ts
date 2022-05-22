import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { BeerListItem } from '../../models';

@Component({
  selector: 'beer-list',
  templateUrl: './beer-list.component.html',
  styleUrls: ['./beer-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeerListComponent {
  @Input() beers: Array<BeerListItem> = [];
  @Output() beerClick = new EventEmitter<string>();

  onBeerClick(id: string): void {
    this.beerClick.emit(id);
  }
}
