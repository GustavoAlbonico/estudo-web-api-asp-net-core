import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';
import { PublicLayout } from './core/layouts/public-layout/public-layout';
import { AuthLayout } from './core/layouts/auth-layout/auth-layout';

export const routes: Routes = [
    {
        path: 'categorias',
        component: AuthLayout,
        canActivate:[authGuard],
        loadChildren: () =>
            import('./features/categorias/categorias.routes')
                .then(categorias => categorias.CATEGORIAS_ROUTES),
    },
    {
        path: 'login',
        component: PublicLayout,
        loadChildren: () =>
            import('./features/auth/auth.routes')
                .then(auth => auth.AUTH_ROUTES),
    },
    { path: '**', redirectTo: 'categorias'},
];
