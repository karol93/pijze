import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'spinner',
  templateUrl: './spinner.component.html',
})
export class SpinnerComponent implements OnChanges {
  @Input() visible = false;

  name = '';

  constructor(private spinner: NgxSpinnerService) {
    this.name = `${Math.random()
      .toString(36)
      .slice(2, 7)}_${new Date().getTime()}`;
  }

  show(name: string) {
    this.spinner.show(name);
  }

  hide(name: string) {
    this.spinner.hide(name);
  }

  ngOnChanges(changes: SimpleChanges): void {
    const showSpinnerChange = changes['visible'];
    if (showSpinnerChange) {
      showSpinnerChange.currentValue
        ? this.show(this.name)
        : this.hide(this.name);
    }
  }
}
