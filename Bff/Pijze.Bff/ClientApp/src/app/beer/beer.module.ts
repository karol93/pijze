import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

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
import { BeerEffects, beerReducer } from './store';

@NgModule({
  imports: [
    StoreModule.forFeature(beerReducer),
    EffectsModule.forFeature([BeerEffects]),
    BeerRoutingModule,
    SharedModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
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
