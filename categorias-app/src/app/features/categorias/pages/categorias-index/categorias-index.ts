import { Component, inject, signal } from '@angular/core';
import { ListTable } from '../../../../shared/components/list-table/list-table';
import { Router } from '@angular/router';
import { CategoriaService } from '../../services/categorias.service';
import { Categoria } from '../../models/categoria.model';

@Component({
  selector: 'app-categorias-index',
  imports: [ListTable],
  templateUrl: './categorias-index.html',
})
export class CategoriasIndex {
  private router = inject(Router);
  private categoriasService = inject(CategoriaService)

  categorias = signal<Categoria[]>([]);
  loading = signal(true);

  columns = [
    { key: 'nome', label: 'Nome' },
    { key: 'imagem', label: 'Imagem'},
  ];

  constructor() {
    this.loadCategorias();
  }

  loadCategorias() {
    this.loading.set(true);

    this.categoriasService.getAll().subscribe({
      next: (data) => {
        this.categorias.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      },
    });
  }
  edit(categoria: Categoria): void {
    this.router.navigate([`categorias/${categoria.id}/editar`]);
  }

  remove(categoria: Categoria): void {
    if (!confirm(`Deseja excluir "${categoria.nome}"?`)) return;

    this.categoriasService.delete(categoria.id).subscribe(() => {
      this.loadCategorias();
    });
  }

}
