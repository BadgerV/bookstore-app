import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Store, select } from '@ngrx/store';
import { AppState } from '../../store/reducers/reducers';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { isLoggedIn } from '../../store/selectors/auth.selector';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private store: Store<AppState>, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return this.store.pipe(
      select(isLoggedIn),
      map((isLoggedIn: boolean) => {
        if (isLoggedIn) {
          return true;
        } else {
          return this.router.createUrlTree(['login']);
        }
      }),
      catchError(() => {
        return of(this.router.createUrlTree(['login']));
      })
    );
  }
}
