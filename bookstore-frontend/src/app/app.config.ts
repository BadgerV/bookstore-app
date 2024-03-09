import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  provideHttpClient,
  withFetch,
} from '@angular/common/http';

import { StoreModule, provideStore } from '@ngrx/store';
import { reducers } from './store/reducers/reducers';
import { authReducers } from './store/reducers/auth.reducer';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from './store/effects/auth.effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideStore(reducers),
    importProvidersFrom(
      StoreModule.forRoot({
        authReducer: authReducers,
      }),
      EffectsModule.forRoot([AuthEffects])
    ),
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(withFetch()),
    provideRouter(routes),
    provideClientHydration(),
    // importProvidersFrom(FormsModule, ReactiveFormsModule),
  ],
};
