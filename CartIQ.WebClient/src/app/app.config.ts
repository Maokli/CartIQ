import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideZonelessChangeDetection } from '@angular/core';
import { apiBaseUrlInterceptor } from './application/interceptors/apiBaseUrl.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZonelessChangeDetection(),
    provideHttpClient(withInterceptors([apiBaseUrlInterceptor])),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes)
  ]
};
