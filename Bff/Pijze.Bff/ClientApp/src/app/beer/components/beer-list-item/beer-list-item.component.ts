import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { BeerListItem } from '../../models';

@Component({
  selector: 'beer-list-item',
  templateUrl: './beer-list-item.component.html',
  styleUrls: ['./beer-list-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeerListItemComponent {
  constructor() {}

  @Input() beer!: BeerListItem;
}
