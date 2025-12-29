import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriaService } from '../../services/categorias.service';
import { PageHeader } from '../../../../shared/components/page-header/page-header';
import { CategoriaRequest } from '../../models/categoria.model';

@Component({
  selector: 'app-categorias-form',
  imports: [ReactiveFormsModule, PageHeader],
  templateUrl: './categorias-form.html',
})
export class CategoriasForm implements OnInit {

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private formBuilder = inject(FormBuilder);
  private categoriaService = inject(CategoriaService);
  private categoriaId?: number;

  isEdit = signal(false);

  form = this.formBuilder.nonNullable.group({
    nome: ['', Validators.required],
    imagemUrl: ['', Validators.required],
  });

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.isEdit.set(true);
      this.categoriaId = Number(id);
      this.loadCategoria(this.categoriaId);
    }
  }

  loadCategoria(id: number) {
    this.categoriaService.getById(id).subscribe({
      next: (data) => {
        this.form.patchValue(data);
      },
      error: () => {

      }
    })
  }

  onSave() {
    if (this.form.invalid) return;

    const categoriaRequest: CategoriaRequest = {
      ...this.form.value,
      id: this.categoriaId
    };

    if (this.isEdit()) {
      this.categoriaService.update(this.categoriaId!, categoriaRequest).subscribe({
        next: (data) => {
          this.router.navigate(['/categorias']);
        },
        error: () => {

        }
      });
    } else {
       this.categoriaService.create(categoriaRequest).subscribe({
        next: (data) => {
          this.router.navigate(['/categorias']);
        },
        error: () => {

        }
      });
    }
  }

  onBack(): void {
    this.router.navigate(['/categorias']);
  }
}
