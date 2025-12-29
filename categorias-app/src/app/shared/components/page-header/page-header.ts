import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-page-header',
  imports: [],
  templateUrl: './page-header.html',
})
export class PageHeader {
  title = input<string>('');
  subtitle = input<string>('');
  actionLabel = input<string>('');

  actionEvent = output<void>();
}
