import { Routes } from '@angular/router';

export const CATEGORIAS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/categorias-index/categorias-index')
        .then(categoria => categoria.CategoriasIndex)
  },
  {
    path: 'nova',
    loadComponent: () =>
      import('./pages/categorias-form/categorias-form')
        .then(categoria => categoria.CategoriasForm)
  },
  {
    path: ':id/editar',
    loadComponent: () =>
      import('./pages/categorias-form/categorias-form')
        .then(categoria => categoria.CategoriasForm)
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./pages/categorias-view/categorias-view')
        .then(categoria => categoria.CategoriasView)
  }
];
