import { createReducer, on } from '@ngrx/store/src';
import { User } from '../../shared/models/UserModel';
import * as AuthActions from '../actions/auth.actions';

export interface AuthState {
  user: User | null;
  isLoggedIn: boolean;
  error: any;
  token: string | null;
}

export const initialState: AuthState = {
  user: null,
  isLoggedIn: false,
  error: null,
  token: null,
};

export const authReducers = createReducer(
  initialState,
  on(AuthActions.loginSuccess, (state, { user }) => ({
    ...state,
    token : user.token,
    user,
    isLoggedIn: true,
  })),
  on(AuthActions.loginFailure, (state, { error }) => ({ ...state, error })),
  on(AuthActions.logoutSuccess, (state) => ({
    ...state,
    isLoggedIn: false,
    user: null,
    error: null,
    token: null,
  })),
  on(AuthActions.logoutFailure, (state, { error }) => ({ ...state, error }))
);
