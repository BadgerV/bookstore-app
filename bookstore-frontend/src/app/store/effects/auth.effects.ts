import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, switchMap, catchError } from 'rxjs/operators';
import * as AuthActions from '../actions/auth.actions';
import { ApiService } from '../../core/services/apiService';

@Injectable()
export class AuthEffects {
  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.login),
      switchMap(({ username, password }) =>
        this.apiService.login(username, password).pipe(
          map((user) => {
            localStorage.setItem('token', user.token);
            return AuthActions.loginSuccess({ user });
          }),
          catchError((error) => of(AuthActions.loginFailure({ error })))
        )
      )
    )
  );

  logout$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.logout),
      switchMap(() => {
        localStorage.removeItem('token');
        return of(AuthActions.logoutSuccess());
      })
    )
  );
  constructor(private apiService: ApiService, private actions$: Actions) {}
}
