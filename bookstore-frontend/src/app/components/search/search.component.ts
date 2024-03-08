import { Component } from '@angular/core';

import { NgFor } from '@angular/common';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SearchResultComponent } from '../../shared/search-result/search-result.component';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [SearchResultComponent, NgFor, FooterComponent],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss',
})
export class SearchComponent {
  searchTitle: string = 'The alchemist';

  searchResults: any[] = [
    {
      name: 'The Alchemist',
      rating: 4.5,
      author: 'Segunmaru Faozan',
      img: '../../../assets/images/book-image-1.png',
    },

    {
      name: 'The Night Circus',
      rating: 4.8,
      author: 'Erin Morgenstern',
      img: '../../../assets/images/book-image-1.png',
    },
    {
      name: 'The Catcher in the Rye',
      rating: 4.3,
      author: 'J.D. Salinger',
      img: '../../../assets/images/book-image-4.png',
    },
    {
      name: 'Pride and Prejudice',
      rating: 4.6,
      author: 'Jane Austen',
      img: '../../../assets/images/book-image-2.png',
    },
    {
      name: 'The Great Gatsby',
      rating: 4.7,
      author: 'F. Scott Fitzgerald',
      img: '../../../assets/images/book-image-4.png',
    },
    {
      name: 'To Kill a Mockingbird',
      rating: 4.5,
      author: 'Harper Lee',
      img: '../../../assets/images/book-image-3.png',
    },
    {
      name: 'The Hobbit',
      rating: 4.4,
      author: 'J.R.R. Tolkien',
      img: '../../../assets/images/book-image-2.png',
    },
  ];
}
