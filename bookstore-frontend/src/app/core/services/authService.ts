import { Store, select } from '@ngrx/store';
import { AppState } from '../../store/reducers/reducers';
import { Injectable } from '@angular/core';
import { isLoggedIn } from '../../store/selectors/auth.selector';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private store: Store<AppState>, private router: Router) {}

  navigateToHomepage(): void {
    this.store
      .select(isLoggedIn)
      .pipe(
        map((isLoggedIn: boolean) => isLoggedIn && this.router.navigate(['']))
      )
      .subscribe();
  }
}
