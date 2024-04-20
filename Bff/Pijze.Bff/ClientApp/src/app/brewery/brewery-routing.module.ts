import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BreweriesComponent, BreweryComponent } from './containers';
import { BreweryExistsGuard } from './guards';
import { BreweriesLoadedGuard } from './guards/breweries-loaded.guard';

const routes: Routes = [
  {
    path: '',
    component: BreweriesComponent,
    canActivate: [BreweriesLoadedGuard],
  },
  {
    path: 'add',
    component: BreweryComponent,
    canActivate: [BreweryExistsGuard],
  },
  {
    path: 'edit/:id',
    component: BreweryComponent,
    canActivate: [BreweryExistsGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class BreweryRoutingModule {}
