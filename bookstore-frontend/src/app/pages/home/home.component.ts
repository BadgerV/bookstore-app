import { Component } from '@angular/core';
import { HeroComponent } from '../../components/hero/hero.component';
import { RecommendedBooksComponent } from '../../components/recommended-books/recommended-books.component';
import { TopRatedBooksComponent } from '../../components/top-rated-books/top-rated-books.component';
import { PopularCategoriesComponent } from '../../components/popular-categories/popular-categories.component';
import { FooterComponent } from '../../components/footer/footer.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeroComponent,
    RecommendedBooksComponent,
    TopRatedBooksComponent,
    PopularCategoriesComponent,
    FooterComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
