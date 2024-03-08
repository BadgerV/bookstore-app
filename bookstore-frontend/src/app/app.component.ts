import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './shared/header/header.component';
import { ApiService } from './core/services/apiService';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  providers: [ApiService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'bookstore-frontend';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getBooks().subscribe((result) => {
      console.log(result);
    });
  }
}
