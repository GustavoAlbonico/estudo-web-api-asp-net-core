import { Component, input } from '@angular/core';
import { required } from '@angular/forms/signals';

@Component({
  selector: 'app-list-table',
  imports: [],
  templateUrl: './list-table.html',
  styleUrl: './list-table.css',
})

interface IListTable {
  thead:string[],
  tbody: {
    fieldName:string[],
    data:object[]
  }
};

export class ListTable {
    tableDatas = input.required<IListTable>()
}
