import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { BeerFilters } from '../../models';

@Component({
  selector: 'beers-filter',
  templateUrl: './beers-filter.component.html',
  styleUrls: ['./beers-filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeersFilterComponent implements OnInit, OnDestroy {
  private formSubscription?: Subscription;

  protected filtersForm = this.formBuilder.group({
    text: [''],
    rating: [null as number | null],
  });

  @Input() filters!: BeerFilters | null;
  @Output() beersFilter = new EventEmitter<BeerFilters | null>();

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    if (this.filters) {
      this.filtersForm.setValue(this.filters);
    }
    this.formSubscription = this.filtersForm.valueChanges.subscribe(
      (filters) => {
        if (!filters.text && !filters.rating) {
          this.beersFilter.emit(null);
        } else {
          this.beersFilter.emit({
            text: filters.text || '',
            rating: filters.rating || 0,
          });
        }
      }
    );
  }

  protected clearFilters(): void {
    this.filtersForm.reset();
  }

  ngOnDestroy(): void {
    this.formSubscription?.unsubscribe();
  }
}
