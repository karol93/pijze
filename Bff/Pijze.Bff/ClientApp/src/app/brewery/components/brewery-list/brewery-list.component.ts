import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { Brewery, Pagination } from 'src/app/core';

@Component({
  selector: 'brewery-list',
  templateUrl: './brewery-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BreweryListComponent {
  @Input() breweries: ReadonlyArray<Brewery> = [];
  @Input() pagination: Pagination | null = null;
  @Output() breweryDeleteActionClick = new EventEmitter<string>();
  @Output() pageChanged = new EventEmitter<number>();

  protected onBreweryDeleteActionClicked(id: string): void {
    this.breweryDeleteActionClick.emit(id);
  }

  protected onPageChange(pageNumber: number): void {
    this.pageChanged.emit(pageNumber);
  }
}
