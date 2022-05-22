import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Attributes, IntersectionObserverHooks } from 'ng-lazyload-image';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LazyLoadImageHooks extends IntersectionObserverHooks {
  private http: HttpClient;

  constructor(http: HttpClient) {
    super();
    this.http = http;
  }

  override loadImage({ imagePath }: Attributes): Observable<string> {
    return this.http
      .get(imagePath, { responseType: 'blob' })
      .pipe(map((blob) => URL.createObjectURL(blob)));
  }
}
