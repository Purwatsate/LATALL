import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const authToken = authService.getToken();

  if (authToken) {
    const cloned = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authToken}`),
    });
    return next(cloned);
  }
  return next(req);
};
