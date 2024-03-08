import { Component } from '@angular/core';
import { FooterComponent } from '../../shared/footer/footer.component';
import { HeroComponent } from '../../shared/hero/hero.component';
import { PopularCategoriesComponent } from '../../shared/popular-categories/popular-categories.component';
import { RecommendedBooksComponent } from '../../shared/recommended-books/recommended-books.component';
import { TopRatedBooksComponent } from '../../shared/top-rated-books/top-rated-books.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeroComponent,
    RecommendedBooksComponent,
    TopRatedBooksComponent,
    PopularCategoriesComponent,
    FooterComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
