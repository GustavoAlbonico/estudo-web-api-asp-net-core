import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categoria } from '../models/categoria.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  private httpClient = inject(HttpClient);

  private readonly API_ENDPOINT_CATEGORIAS = `${environment.apiCategorias}/api/categorias`;

  getAll(): Observable<Categoria[]> {
    return this.httpClient.get<Categoria[]>(this.API_ENDPOINT_CATEGORIAS);
  }

  delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.API_ENDPOINT_CATEGORIAS}/${id}`);
  }

}
