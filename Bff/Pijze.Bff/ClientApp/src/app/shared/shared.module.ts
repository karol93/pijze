import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { LazyLoadImageModule, LAZYLOAD_IMAGE_HOOKS } from 'ng-lazyload-image';
import { NgxSpinnerModule } from 'ngx-spinner';

import {
  PaginatorComponent,
  RatingComponent,
  SpinnerComponent,
} from './components';
import { LazyLoadImageHooks } from './hooks';
import { AuthImagePipe, ChunkPipe, NumberToArrayPipe } from './pipes';

@NgModule({
  imports: [
    ReactiveFormsModule,
    CommonModule,
    LazyLoadImageModule,
    NgxSpinnerModule,
  ],
  declarations: [
    ChunkPipe,
    NumberToArrayPipe,
    RatingComponent,
    AuthImagePipe,
    SpinnerComponent,
    PaginatorComponent,
  ],
  exports: [
    ReactiveFormsModule,
    ChunkPipe,
    RatingComponent,
    NumberToArrayPipe,
    AuthImagePipe,
    LazyLoadImageModule,
    SpinnerComponent,
    PaginatorComponent,
  ],
  providers: [{ provide: LAZYLOAD_IMAGE_HOOKS, useClass: LazyLoadImageHooks }],
})
export class SharedModule {}
