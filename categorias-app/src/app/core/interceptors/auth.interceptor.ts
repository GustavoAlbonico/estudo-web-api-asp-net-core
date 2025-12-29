import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';


export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);

  const token = auth.accessToken();

  const authReq = token
    ? req.clone({
      setHeaders: { Authorization: `Bearer ${token}` },
      withCredentials: true
    })
    : req.clone({ withCredentials: true });

  return next(authReq);
};
