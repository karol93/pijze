import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BeerComponent, BeersComponent } from './containers';
import {
  BeerExistsGuard,
  BeersLoadedGuard,
  BreweriesLoadedGuard,
} from './guards';

const routes: Routes = [
  {
    path: '',
    component: BeersComponent,
    canActivate: [BeersLoadedGuard],
  },
  {
    path: 'add',
    component: BeerComponent,
    canActivate: [BeerExistsGuard, BreweriesLoadedGuard],
  },
  {
    path: 'edit/:id',
    component: BeerComponent,
    canActivate: [BeerExistsGuard, BreweriesLoadedGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class BeerRoutingModule {}
