import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { Pagination } from 'src/app/core';

@Component({
  selector: 'paginator',
  templateUrl: './paginator.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginatorComponent implements OnInit {
  private _pagination!: Pagination;

  protected isPrevPageButtonDisabled: boolean = false;
  protected isNextPageButtonDisabled: boolean = false;

  @Input({ required: true })
  get pagination() {
    return this._pagination;
  }

  set pagination(pagination: Pagination) {
    this._pagination = pagination;
    this.setButtonsVisiblity();
  }

  @Output() pageChanged = new EventEmitter<number>();

  ngOnInit(): void {
    this.setButtonsVisiblity();
  }

  onPageChange(pageNumber: number): void {
    this.pageChanged.emit(pageNumber);
  }

  private setButtonsVisiblity(): void {
    this.isPrevPageButtonDisabled = this.pagination.currentPage <= 1;
    this.isNextPageButtonDisabled =
      this.pagination.currentPage * this.pagination.itemsPerPage >=
      this.pagination.totalItems;
  }
}
