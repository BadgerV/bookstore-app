import { Component } from '@angular/core';
import { Book } from '../models/BookModel';
import { BookCardComponent } from '../book-card/book-card.component';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-top-rated-books',
  standalone: true,
  imports: [BookCardComponent, NgFor],
  templateUrl: './top-rated-books.component.html',
  styleUrl: './top-rated-books.component.scss',
})
export class TopRatedBooksComponent {
  books: Book[] = [
    {
      name: "Entropy's Dance",
      author: 'Segunmaru Faozan',
      img: '../../../assets/images/book-image-1.png',
    },
    {
      name: 'The Fortune',
      author: 'Segunmaru Fridaoz',
      img: '../../../assets/images/book-image-2.png',
    },
    {
      name: 'Taiwo and Kahidne',
      author: 'Segunmaru Fawaz',
      img: '../../../assets/images/book-image-3.png',
    },
    {
      name: 'The Fortune',
      author: 'Segunmaru Fridaoz',
      img: '../../../assets/images/book-image-4.png',
    },
    {
      name: 'The Fortune',
      author: 'Segunmaru Fridaoz',
      img: '../../../assets/images/book-image-2.png',
    },
  ];
}
