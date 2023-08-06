import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'beer-image',
  templateUrl: './beer-image.component.html',
  styleUrls: ['./beer-image.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeerImageComponent {
  @Input() beerId?: string;
}
