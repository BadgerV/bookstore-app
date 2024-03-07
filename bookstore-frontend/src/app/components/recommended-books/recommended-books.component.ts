import { Component } from '@angular/core';
import { BookCardComponent } from '../book-card/book-card.component';
import { NgFor } from '@angular/common';
import { Book } from '../../models/BookModel';

@Component({
  selector: 'app-recommended-books',
  standalone: true,
  imports: [BookCardComponent, NgFor],
  templateUrl: './recommended-books.component.html',
  styleUrl: './recommended-books.component.scss',
})
export class RecommendedBooksComponent {
  books : Book[] = [
    {
      name: "Entropy's Dance",
      author: 'Segunmaru Faozan',
      img: '../../../assets/images/book-image-1.png',
    },
    {
      name: "The Fortune",
      author: 'Segunmaru Fridaoz',
      img: '../../../assets/images/book-image-2.png',
    },
    {
      name: 'Taiwo and Kahidne',
      author: 'Segunmaru Fawaz',
      img: '../../../assets/images/book-image-3.png',
    },
    {
      name: "The Fortune",
      author: 'Segunmaru Fridaoz',
      img: '../../../assets/images/book-image-4.png',
    },
    {
      name: "The Fortune",
      author: 'Segunmaru Fridaoz',
      img: '../../../assets/images/book-image-2.png',
    },
  ];
}
