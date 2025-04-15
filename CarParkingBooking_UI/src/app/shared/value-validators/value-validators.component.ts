import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-value-validators',
  templateUrl: './value-validators.component.html',
  styleUrls: ['./value-validators.component.css'],
})
export class ValueValidatorsComponent implements OnInit {
  @Input() value: any | null;
  protected _value: any;

  constructor() {}

  ngOnInit() {
    this._value = 'N/A';
    var index = 0;
    const valueHandlers: Record<string, () => string> = {
      Date: () => {
        const date = new Date(this.value);
        const options: Intl.DateTimeFormatOptions = {
          day: '2-digit',
          month: 'short',
          year: 'numeric',
          hour: '2-digit',
          minute: '2-digit',
          hour12: true,
          timeZone: 'UTC',
        };
        const formattedDate = date.toLocaleString('en-GB', options).replace(',', '');
        const [day, month, year, time] = formattedDate.split(' ');
        return `${day} ${month} ${year}, ${time}`;
      },
      default: () => this.value.toString(),
    };

     if (this.value?.toString().trim()) {
      const isDate = this.isStrictDate(this.value);
      const type = isDate ? 'Date' : 'default';
      this._value = valueHandlers[type]();
    }
  }

  isStrictDate(val: any): boolean {
    if (val instanceof Date && !isNaN(val.getTime())) {
      return true;
    }
    if (typeof val === 'string') {
      // Only allow clearly formatted date strings like "2024-04-10T12:00:00Z"
      const isoFormat = /^\d{4}-\d{2}-\d{2}(T.*)?$/;
      return isoFormat.test(val) && !isNaN(new Date(val).getTime());
    }
    return false;
  }
}
