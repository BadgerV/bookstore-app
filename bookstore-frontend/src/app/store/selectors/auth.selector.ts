import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from '../reducers/auth.reducer';

export const selectAuthState = createFeatureSelector<AuthState>('authReducer');

export const isLoggedIn = createSelector(
  selectAuthState,
  (authReducer) => authReducer.isLoggedIn
);

export const user = createSelector(
  selectAuthState,
  (authReducer) => authReducer.user
);

export const token = createSelector(
  selectAuthState,
  (authReducer) => authReducer.token
);

export const error = createSelector(
  selectAuthState,
  (authReducer) => authReducer.error
);
