import { NgIf } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl : "./header.component.html",
  imports: [NgIf],
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  isLoggedIn = false;
}
