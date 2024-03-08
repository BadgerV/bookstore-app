export interface User {
  username: string;
  email: string;
  createdAt: string;
  isAuthor: string;
  firstName?: string;
  lastName?: string;
  phoneNumber?: string;
  id: number;
  shippingAddress?: string;
  token: string;
}
