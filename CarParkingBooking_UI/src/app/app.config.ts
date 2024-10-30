import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ErrorInterceptor } from './custom_components/error_toast/HttpInterceptor/error-response.service';
import {imageConfig} from "../WarningBlock";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
              provideHttpClient(withInterceptors([ErrorInterceptor])),
              imageConfig
  ]
};
