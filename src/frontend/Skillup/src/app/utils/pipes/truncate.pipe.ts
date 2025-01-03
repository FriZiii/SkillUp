import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate',
  standalone: true,
})
export class TruncatePipe implements PipeTransform {
  transform(value: string, limit: number = 32): string {
    if (value.length <= limit) {
      return value;
    }
    return value.substring(0, limit) + '...';
  }
}


@Pipe({
  name: 'truncate2',
  standalone: true,
})
export class Truncate2Pipe implements PipeTransform {
  transform(value: string, limit: number = 40): string {
    if (value.length <= limit) {
      return value;
    }
    return value.substring(0, limit) + '...';
  }
}
