import { createReducer, on } from '@ngrx/store';
import { User } from '../../shared/models/UserModel';
import * as AuthActions from '../actions/auth.actions';

export interface AuthState {
  user: User | null;
  isLoggedIn: boolean;
  error: any;
  token: string | null;
}

// Retrieve token only if localStorage is available (in a browser environment)
const token =
  typeof window !== 'undefined' ? localStorage.getItem('token') : null;

export const initialState: AuthState = {
  user: null,
  isLoggedIn: !!token, // Initialize isLoggedIn based on the presence of a token
  error: null,
  token: null,
};

export const authReducers = createReducer(
  initialState,
  on(AuthActions.loginSuccess, (state, { user }) => ({
    ...state,
    token: user.token, // Assuming user object contains a token property
    user,
    isLoggedIn: true, // Update isLoggedIn to true upon successful login
  })),
  on(AuthActions.loginFailure, (state, { error }) => ({ ...state, error })),
  on(AuthActions.logoutSuccess, (state) => ({
    ...state,
    isLoggedIn: false, // Update isLoggedIn to false upon successful logout
    user: null,
    error: null,
    token: null,
  })),
  on(AuthActions.logoutFailure, (state, { error }) => ({ ...state, error }))
);
