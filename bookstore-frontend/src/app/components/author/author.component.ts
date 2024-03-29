import { Component } from '@angular/core';
import { Book } from '../../shared/models/BookModel';
import { BookCardComponent } from '../../shared/book-card/book-card.component';
import { NgFor } from '@angular/common';
import { FooterComponent } from '../../shared/footer/footer.component';

@Component({
  selector: 'app-author',
  standalone: true,
  imports: [BookCardComponent, NgFor, FooterComponent],
  templateUrl: './author.component.html',
  styleUrl: './author.component.scss',
})
export class AuthorComponent {
  bestBookSoFar: string = "Entropy's Dance";

  authorSocials = {
    twitter: '@FaozyBG',
    youtube: '@FaozyBG',
    facebook: '@FaozyBG',
    insta: '@FaozyBG',
  };

  booksByAuthor: Book[] = [
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
