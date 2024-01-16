import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
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

  filtersForm!: UntypedFormGroup;

  @Output() beersFilter = new EventEmitter<BeerFilters | null>();

  constructor(private formBuilder: UntypedFormBuilder) {}

  ngOnInit(): void {
    this.filtersForm = this.formBuilder.group({
      text: [''],
      rating: [''],
    });
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

  clearFilters(): void {
    this.filtersForm.reset();
  }

  ngOnDestroy(): void {
    this.formSubscription?.unsubscribe();
  }
}
