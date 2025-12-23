import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
    {
        path: 'categorias',
        canActivate:[authGuard],
        loadChildren: () =>
            import('./features/categorias/categorias.routes')
                .then(categorias => categorias.CATEGORIAS_ROUTES),
    },
    {
        path: '',
        loadChildren: () =>
            import('./features/auth/auth.routes')
                .then(auth => auth.AUTH_ROUTES),
    },
    { path: '', redirectTo: 'categorias', pathMatch: 'full' },
];
