import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Sidebar } from '../sidebar/sidebar';

@Component({
  selector: 'app-auth-layout',
  imports: [RouterOutlet, Sidebar],
  templateUrl: './auth-layout.html',
})
export class AuthLayout {

}
