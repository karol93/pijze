import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { SharedModule } from '../shared';
import { BreweryRoutingModule } from './brewery-routing.module';
import { BreweryEffects, breweryReducer } from './store';
import { BreweriesComponent, BreweryComponent } from './containers';
import { BreweryEditorComponent, BreweryListComponent } from './components';

@NgModule({
  imports: [
    StoreModule.forFeature(breweryReducer),
    EffectsModule.forFeature([BreweryEffects]),
    BreweryRoutingModule,
    SharedModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  declarations: [
    BreweriesComponent,
    BreweryComponent,
    BreweryListComponent,
    BreweryEditorComponent,
  ],
})
export class BreweryModule {}
