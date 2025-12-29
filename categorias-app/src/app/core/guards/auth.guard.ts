import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { catchError, map, of } from 'rxjs';

export const authGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.isAuthenticated()) {
    return true;
  }

  return auth.refresh().pipe(//pipe -> encadeador de operadores RxJS - necessario para minipular operações assincronas relacionadas a um obervable
    map(res => {
      auth.setAccessToken(res.accessToken);
      return true;
    }),
    catchError(() => {
      router.navigate(['/login']);
      return of(false);//retornar um observable false
    })
  );
};
