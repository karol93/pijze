import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { Brewery } from 'src/app/core';

@Component({
  selector: 'brewery-editor',
  templateUrl: './brewery-editor.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BreweryEditorComponent implements OnInit {
  constructor(private formBuilder: NonNullableFormBuilder) {}

  protected breweryForm = this.formBuilder.group({
    id: [''],
    name: ['', Validators.required],
  });

  imageSrc: string = '';

  @Output() brewerySave = new EventEmitter<Brewery>();
  @Input() brewery!: Brewery | null;

  ngOnInit(): void {
    if (this.brewery) {
      this.breweryForm.patchValue(this.brewery);
    }
  }

  protected onSubmit(): void {
    if (this.breweryForm.valid) {
      this.brewerySave.emit({
        name: this.breweryForm.value.name!,
        id: this.breweryForm.value.id,
      });
    } else {
      this.breweryForm.markAllAsTouched();
    }
  }
}
