import { HttpClient } from '@angular/common/http';
import { Pipe, PipeTransform } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Pipe({
  name: 'authImage',
})
export class AuthImagePipe implements PipeTransform {
  constructor(private http: HttpClient) {}

  transform(src: string): Observable<string> {
    return this.http.get(src, { responseType: 'blob' }).pipe(
      switchMap((imageBlob) => {
        return this.blobToBase64(imageBlob);
      })
    );
  }

  private blobToBase64(imageBlob: Blob) {
    const reader = new FileReader();
    return new Observable((observer: Subscriber<string>): void => {
      reader.onloadend = () => {
        observer.next(reader.result as string);
        observer.complete();
      };
      reader.readAsDataURL(imageBlob);
    });
  }
}
