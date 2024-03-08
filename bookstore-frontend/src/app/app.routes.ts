import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/auth/login/login.component';
import { AddBookComponent } from './components/add-book/add-book.component';
import { SearchComponent } from './components/search/search.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { AuthorComponent } from './components/author/author.component';
import { BookPageComponent } from './components/book-page/book-page.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
// import { HomeComponent } from './pages/home/home.component';
// import { LoginComponent } from './components/auth/login/login.component';
// import { AddBookComponent } from './components/add-book/add-book.component';
// import { SearchComponent } from './pages/search/search.component';
// import { SignupComponent } from './components/auth/signup/signup.component';
// import { AuthorComponent } from './pages/author/author.component';
// import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
// import { BookPageComponent } from './pages/book-page/book-page.component';

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
    path: 'author-page/:id',
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
