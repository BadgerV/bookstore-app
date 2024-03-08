import { Component, OnInit } from '@angular/core';

import { NgFor } from '@angular/common';
import { Review } from '../../shared/models/ReviewModel';

import { calculateStarPercentage } from '../../utils/helperFunction';
import { Percentages } from '../../shared/models/PercentagesModel';
import { FooterComponent } from '../../shared/footer/footer.component';
import { ReviewComponent } from '../../shared/review/review.component';

@Component({
  selector: 'app-book-page',
  standalone: true,
  imports: [FooterComponent, NgFor, ReviewComponent],
  templateUrl: './book-page.component.html',
  styleUrl: './book-page.component.scss',
})
export class BookPageComponent implements OnInit {
  stars: number = 4;
  percentages!: Percentages;

  reviews: Review[] = [
    {
      username: 'BookLover23',
      date: '2023-05-15',
      stars: 1,
      comment:
        'I found this book quite enjoyable overall. The plot was engaging, and the characters were well-developed. However, there were a few instances where the story seemed to drag on, and I felt that some parts could have been condensed. Despite this, I would still recommend it to others.',
      likes: 23,
      dislikes: 5,
    },
    {
      username: 'ReadsALot',
      date: '2023-06-20',
      stars: 1,
      comment:
        "This book completely exceeded my expectations! The writing style was captivating, and I couldn't put it down. The characters felt so real, and the plot twists kept me on the edge of my seat. I finished it in one sitting and immediately wanted to read it again. Highly recommended!",
      likes: 50,
      dislikes: 1,
    },
    {
      username: 'Bookworm77',
      date: '2023-07-02',
      stars: 3,
      comment:
        "I had mixed feelings about this book. While the premise was intriguing, I felt that the execution fell short. The pacing was inconsistent, and some parts felt rushed while others dragged on. Additionally, I found it difficult to connect with the characters. Overall, it was an okay read, but it didn't leave a lasting impression.",
      likes: 12,
      dislikes: 7,
    },
    {
      username: 'LiteraryLover',
      date: '2023-08-10',
      stars: 5,
      comment:
        "Unfortunately, I was quite disappointed by this book. The plot was predictable, and the characters lacked depth. I found myself struggling to stay engaged, and I ended up skimming through several chapters. Overall, it felt like a missed opportunity, and I wouldn't recommend it.",
      likes: 5,
      dislikes: 20,
    },
    {
      username: 'Bibliophile99',
      date: '2023-09-05',
      stars: 1,
      comment:
        "This book was absolutely incredible! From the very first page, I was hooked. The world-building was masterfully done, and the characters were so well-developed. I laughed, I cried, and I couldn't stop thinking about it long after I finished. It's definitely one of my all-time favorites!",
      likes: 78,
      dislikes: 0,
    },
    {
      username: 'BookAddict101',
      date: '2023-10-12',
      stars: 3,
      comment:
        'Overall, I really enjoyed reading this book. The plot was intriguing, and the writing style was engaging. However, there were a few moments where the story felt a bit cliché, and I wished for more originality. Despite this, I would still recommend it to anyone looking for a fun and light-hearted read.',
      likes: 32,
      dislikes: 3,
    },
    {
      username: 'NovelEnthusiast',
      date: '2023-11-28',
      stars: 2,
      comment:
        "I had high hopes for this book, but unfortunately, it didn't quite meet my expectations. While the premise was promising, I found the execution to be lacking. The characters felt one-dimensional, and the plot twists were predictable. It wasn't terrible, but it also didn't leave a lasting impression.",
      likes: 15,
      dislikes: 8,
    },
  ];

  ngOnInit(): void {
    this.percentages = calculateStarPercentage(this.reviews);
  }
}
