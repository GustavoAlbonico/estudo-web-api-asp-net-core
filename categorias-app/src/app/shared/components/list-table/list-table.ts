import { Component, input, output } from '@angular/core';
import { TableColumn } from '../../models/table-columnm.model';

@Component({
  selector: 'app-list-table',
  imports: [],
  templateUrl: './list-table.html',
})

export class ListTable<T extends Record<string, any>> {
  items = input<T[]>([]);
  columns = input<TableColumn[]>([]);
  showActions = input<boolean>(true);

  edit = output<T>();
  remove = output<T>();

  alignClass(align?: string) {
    switch (align) {
      case 'center':
        return 'text-center';
      case 'right':
        return 'text-right';
      default:
        return 'text-left';
    }
  }
}