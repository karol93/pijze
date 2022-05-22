import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'numberToArray' })
export class NumberToArrayPipe implements PipeTransform {
  transform(value: number): Array<number> {
    let res: Array<number> = [];
    if (value <= 0) return res;
    for (let i = 0; i < value; i++) {
      res.push(i);
    }
    return res;
  }
}
