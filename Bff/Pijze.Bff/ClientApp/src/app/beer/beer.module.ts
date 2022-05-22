import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { SharedModule } from '../shared';
import { BeerRoutingModule } from './beer-routing.module';
import {
  BeerEditorComponent,
  BeerImageComponent,
  BeerListComponent,
  BeerListItemComponent,
  BeersFilterComponent,
} from './components';
import { BeerComponent, BeersComponent } from './containers';

@NgModule({
  imports: [
    BeerRoutingModule,
    SharedModule,
    MatCardModule,
    CommonModule,
    FlexLayoutModule,
    MatButtonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatSnackBarModule,
  ],
  declarations: [
    BeerListComponent,
    BeerEditorComponent,
    BeerComponent,
    BeersComponent,
    BeerListItemComponent,
    BeersFilterComponent,
    BeerImageComponent,
  ],
})
export class BeerModule {}
