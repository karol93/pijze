import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BeerComponent, BeersComponent } from './containers';
import { BeerResolver, BeersResolver } from './resolvers';

const routes: Routes = [
  {
    path: '',
    component: BeersComponent,
  },
  {
    path: 'add',
    component: BeerComponent,
  },
  {
    path: 'edit/:id',
    component: BeerComponent,
    resolve: {
      beer: BeerResolver,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class BeerRoutingModule {}
