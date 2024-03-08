import { Component } from '@angular/core';
import { BookCardComponent } from '../book-card/book-card.component';
import { Book } from '../models/BookModel';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-popular-categories',
  standalone: true,
  imports: [BookCardComponent, NgFor],
  templateUrl: './popular-categories.component.html',
  styleUrl: './popular-categories.component.scss',
})
export class PopularCategoriesComponent {
  books: any[] = [
    {
      name: 'Horror',
      img: '../../../assets/images/book-image-1.png',
    },
    {
      name: 'Self Helf',
      img: '../../../assets/images/book-image-2.png',
    },
    {
      name: 'History',
      img: '../../../assets/images/book-image-3.png',
    },
    {
      name: 'Mystery',
      img: '../../../assets/images/book-image-4.png',
    },
    {
      name: 'Drama',
      img: '../../../assets/images/book-image-2.png',
    },
  ];
}
