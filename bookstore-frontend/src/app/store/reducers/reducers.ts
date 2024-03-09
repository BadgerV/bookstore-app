import { combineReducers } from '@ngrx/store';
import { AuthState, authReducers } from './auth.reducer';

export interface AppState {
  authReducer: AuthState;
}

export const reducers = combineReducers({
  authReducer: authReducers,
});
