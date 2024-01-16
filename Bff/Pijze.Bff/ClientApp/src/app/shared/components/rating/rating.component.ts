import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Input,
  OnInit,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: RatingComponent,
    },
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RatingComponent implements ControlValueAccessor {
  constructor(private changeDetectorRef: ChangeDetectorRef) {}

  maxRating = 5;
  hoverRating = 0;
  @Input() rating: number | null = null;
  @Input() readOnly = false;
  @Input() displayLabel = false;

  onChange = (rating: number | null) => {};
  onTouched = () => {};
  touched = false;
  disabled = false;

  onClick(value: number) {
    this.markAsTouched();
    this.rating = value;
    this.onChange(this.rating);
  }

  writeValue(rating: number | null) {
    this.rating = rating;
    this.changeDetectorRef.detectChanges();
  }

  registerOnChange(onChange: any) {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: any) {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }

  onStartHover(value: number) {
    if (this.readOnly) return;
    this.hoverRating = value;
  }

  onStopHover() {
    this.hoverRating = 0;
  }
}
