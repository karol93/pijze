import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { LazyLoadImageModule } from 'ng-lazyload-image';

import { AppHeaderComponent } from './app-header';
import { CacheInterceptor } from './interceptors';

export abstract class EnsureImportedOnceModule {
  protected constructor(targetModule: any) {
    if (targetModule) {
      throw new Error(
        `${targetModule.constructor.name} has already been loaded.`
      );
    }
  }
}

@NgModule({
  imports: [CommonModule, HttpClientModule, RouterModule, LazyLoadImageModule],
  declarations: [AppHeaderComponent],
  exports: [AppHeaderComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CacheInterceptor,
      multi: true,
    },
  ],
})
export class CoreModule extends EnsureImportedOnceModule {
  public constructor(@SkipSelf() @Optional() parent: CoreModule) {
    super(parent);
  }
}
