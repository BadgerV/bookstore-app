import { Component, Input, OnInit } from '@angular/core';
import { Book } from '../models/BookModel';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [NgIf],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.scss',
})
export class BookCardComponent {
  @Input() book!: Book;
  @Input() isOfTypeCategory!: boolean;
}
