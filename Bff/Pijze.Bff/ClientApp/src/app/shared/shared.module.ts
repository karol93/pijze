import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { MatIconModule } from '@angular/material/icon';
import { LazyLoadImageModule, LAZYLOAD_IMAGE_HOOKS } from 'ng-lazyload-image';

import { RatingComponent } from './components';
import { LazyLoadImageHooks } from './hooks';
import { AuthImagePipe, ChunkPipe, NumberToArrayPipe } from './pipes';

@NgModule({
  imports: [
    ReactiveFormsModule,
    MatIconModule,
    CommonModule,
    LazyLoadImageModule,
  ],
  declarations: [ChunkPipe, NumberToArrayPipe, RatingComponent, AuthImagePipe],
  exports: [
    ReactiveFormsModule,
    ChunkPipe,
    RatingComponent,
    NumberToArrayPipe,
    AuthImagePipe,
    LazyLoadImageModule,
  ],
  providers: [{ provide: LAZYLOAD_IMAGE_HOOKS, useClass: LazyLoadImageHooks }],
})
export class SharedModule {}
