import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ToastsService } from '../toasts.service';

export const ErrorInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const alertService = inject(ToastsService); // Inject the AlertService

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error instanceof HttpErrorResponse) {
        alertService.showToast(`Error: ${error.message}`); // Show the error message
      }
      return throwError(() => error); // Throw the error for further handling
    })
  );
};