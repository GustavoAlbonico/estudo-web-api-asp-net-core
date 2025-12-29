import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
})
export class Login {

  private authService = inject(AuthService);
  private router = inject(Router);

  userName = signal('');
  password = signal('');
  loading = signal(false);
  error = signal<string | null>(null);

  submit() {
    this.loading.set(true);
    this.error.set(null);

    this.authService
      .login({
        userName: this.userName(),
        password: this.password(),
      })
      .subscribe({
        next: (res) => {
          this.authService.setAccessToken(res.token);
          this.router.navigate(['/']);
        },
        error: () => {
          this.error.set('Usuário ou senha inválidos');
          this.loading.set(false);
        },
      });
  }

  updateSignal(signalRef: any, event: Event) {
    signalRef.set((event.target as HTMLInputElement).value);
  }
}