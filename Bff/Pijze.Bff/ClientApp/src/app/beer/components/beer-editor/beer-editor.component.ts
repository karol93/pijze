import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Beer } from '../../models';

@Component({
  selector: 'beer-editor',
  templateUrl: './beer-editor.component.html',
  styleUrls: ['./beer-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BeerEditorComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private changeDetectorRef: ChangeDetectorRef,
    private _snackBar: MatSnackBar
  ) {}

  beerForm!: FormGroup;
  imageSrc: string = '';

  @Output() beerSave = new EventEmitter<Beer>();
  @Output() beerDelete = new EventEmitter<Beer>();
  @Input() beer?: Beer;

  ngOnInit(): void {
    this.beerForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      manufacturer: ['', Validators.required],
      rating: ['', Validators.required],
      photo: ['', Validators.required],
    });
    if (this.beer) {
      this.beerForm.patchValue(this.beer);
      this.imageSrc = this.beer.photo;
    }
  }

  onFileSelected(): void {
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

  onSubmit(): void {
    console.log(this.beerForm);
    if (this.beerForm.valid) {
      this.beerSave.emit(this.beerForm.value);
    } else {
      this.beerForm.markAllAsTouched();
      if (this.beerForm.controls['photo'].invalid) {
        this._snackBar.open('The photo of beer is required', '', {
          duration: 5000,
        });
      }
    }
  }

  onDelete(): void {
    this.beerDelete.emit(this.beer!);
  }
}
