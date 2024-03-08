import { Review } from '../models/ReviewModel';

export function calculateStarPercentage(reviews: Review[]): {
  [key: string]: number;
} {
  const starCounts: { [key: string]: number } = {
    one: 0,
    two: 0,
    three: 0,
    four: 0,
    five: 0,
    average: 0,
  };

  // Count the number of each star rating
  for (const review of reviews) {
    switch (review.stars) {
      case 1:
        starCounts['one']++;
        break;
      case 2:
        starCounts['two']++;
        break;
      case 3:
        starCounts['three']++;
        break;
      case 4:
        starCounts['four']++;
        break;
      case 5:
        starCounts['five']++;
        break;
      default:
        break;
    }
  }

  // Calculate the percentage of each star rating
  const totalReviews = reviews.length;
  const percentages: { [key: string]: number } = {};
  for (const key in starCounts) {
    if (starCounts.hasOwnProperty(key)) {
      percentages[key] = Math.floor((starCounts[key] / totalReviews) * 100);
    }
  }

  const totalStars = reviews.reduce(
    (total: number, review: Review) => total + review.stars,
    0
  );

  percentages['average'] = totalStars / totalReviews;

  return percentages;
}
