import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'beer-image',
  templateUrl: './beer-image.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeerImageComponent {
  @Input() beerId?: string;
}
