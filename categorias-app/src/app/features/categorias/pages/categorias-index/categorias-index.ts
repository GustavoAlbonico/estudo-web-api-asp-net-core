import { Component, inject, signal } from '@angular/core';
import { ListTable } from '../../../../shared/components/list-table/list-table';
import { Router } from '@angular/router';
import { CategoriaService } from '../../services/categorias.service';
import { CategoriaResponse } from '../../models/categoria.model';
import { PageHeader } from '../../../../shared/components/page-header/page-header';

@Component({
  selector: 'app-categorias-index',
  imports: [ListTable, PageHeader],
  templateUrl: './categorias-index.html',
})
export class CategoriasIndex {
  private router = inject(Router);
  private categoriasService = inject(CategoriaService)

  categorias = signal<CategoriaResponse[]>([]);
  loading = signal(true);

  columns = [
    { key: 'nome', label: 'Nome' },
    { key: 'imagem', label: 'Imagem' },
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

  onCreate(): void {
    this.router.navigate([`categorias/nova`]);
  }

  onEdit(categoria: CategoriaResponse): void {
    this.router.navigate([`categorias/${categoria.id}/editar`]);
  }

  onRemove(categoria: CategoriaResponse): void {
    if (!confirm(`Deseja excluir "${categoria.nome}"?`)) return;

    this.categoriasService.delete(categoria.id).subscribe({
      next: () => {
        this.loadCategorias();
      },
      error: () => {
        alert(`Não foi possivel excluir "${categoria.nome}"!`);
      },
    });
  }

}
