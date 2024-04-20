import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core';

const routes: Routes = [
  {
    path: 'beer',
    loadChildren: () => import('./beer/beer.module').then((m) => m.BeerModule),
    canActivate: [AuthGuard],
    title: 'Beers',
  },
  {
    path: 'brewery',
    loadChildren: () =>
      import('./brewery/brewery.module').then((m) => m.BreweryModule),
    canActivate: [AuthGuard],
    title: 'Breweries',
  },
  { path: '', redirectTo: 'beer', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
