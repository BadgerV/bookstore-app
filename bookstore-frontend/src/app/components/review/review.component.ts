import { Component, Input } from '@angular/core';
import { Review } from '../../models/ReviewModel';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [NgFor],
  templateUrl: './review.component.html',
  styleUrl: './review.component.scss',
})
export class ReviewComponent {
  @Input() review!: Review;
}
