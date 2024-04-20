import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { Beer, Brewery } from 'src/app/core';

@Component({
  selector: 'beer-editor',
  templateUrl: './beer-editor.component.html',
  styleUrls: ['./beer-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeerEditorComponent implements OnInit {
  constructor(
    private formBuilder: NonNullableFormBuilder,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  protected beerForm = this.formBuilder.group({
    id: [''],
    name: ['', Validators.required],
    breweryId: ['', Validators.required],
    rating: [null as number | null, Validators.required],
    photo: ['', Validators.required],
  });

  protected imageSrc: string = '';

  @Output() beerSave = new EventEmitter<Beer>();
  @Output() beerDelete = new EventEmitter<Beer>();
  @Input() beer?: Beer | null;
  @Input() breweries?: ReadonlyArray<Brewery>;

  ngOnInit(): void {
    if (this.beer) {
      this.beerForm.patchValue(this.beer);
      this.imageSrc = this.beer.photo;
    }
  }

  protected onFileSelected(): void {
    const inputNode: any = document.querySelector('#file');

    if (typeof FileReader !== 'undefined') {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        const base64 = e.target.result.split('base64,')[1];
        this.imageSrc = base64;
        this.beerForm.patchValue({
          photo: base64,
        });
        this.changeDetectorRef.detectChanges();
      };

      reader.readAsDataURL(inputNode.files[0]);
    }
  }

  protected onSubmit(): void {
    if (this.beerForm.valid) {
      this.beerSave.emit({
        id: this.beerForm.value.id,
        breweryId: this.beerForm.value.breweryId!,
        name: this.beerForm.value.name!,
        photo: this.beerForm.value.photo!,
        rating: this.beerForm.value.rating!,
      });
    } else {
      this.beerForm.markAllAsTouched();
      if (this.beerForm.controls['photo'].invalid) {
        alert('The photo of beer is required');
      }
    }
  }

  protected onDelete(): void {
    this.beerDelete.emit(this.beer!);
  }
}
