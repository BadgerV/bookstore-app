import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AddBookComponent } from './pages/add-book/add-book.component';
import { SearchComponent } from './pages/search/search.component';
import { SignupComponent } from './pages/signup/signup.component';
import { AuthorComponent } from './pages/author/author.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { BookPageComponent } from './pages/book-page/book-page.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'add-book',
    component: AddBookComponent,
  },
  {
    path: 'search',
    component: SearchComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'author-page',
    component: AuthorComponent,
  },
  {
    path: 'book-page/:id',
    component: BookPageComponent,
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];
