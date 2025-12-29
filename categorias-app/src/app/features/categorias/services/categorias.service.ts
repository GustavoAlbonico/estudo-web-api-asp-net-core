import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriaRequest, CategoriaResponse } from '../models/categoria.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  private httpClient = inject(HttpClient);

  private readonly API_ENDPOINT_CATEGORIAS = `${environment.apiCategorias}/api/categorias`;

  getAll(): Observable<CategoriaResponse[]> {
    return this.httpClient.get<CategoriaResponse[]>(this.API_ENDPOINT_CATEGORIAS);
  }

  getById(id: number): Observable<CategoriaResponse> {
    return this.httpClient.get<CategoriaResponse>(`${this.API_ENDPOINT_CATEGORIAS}/${id}`);
  }

  create(categoriaRequest: CategoriaRequest): Observable<CategoriaResponse> {
    return this.httpClient.post<CategoriaResponse>(this.API_ENDPOINT_CATEGORIAS, categoriaRequest);
  }

  update(id: number, categoriaRequest: CategoriaRequest): Observable<CategoriaResponse> {
    return this.httpClient.put<CategoriaResponse>(`${this.API_ENDPOINT_CATEGORIAS}/${id}`, categoriaRequest);
  }

  delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.API_ENDPOINT_CATEGORIAS}/${id}`);
  }

}
