import { Component, signal } from '@angular/core';
import {Router, RouterLink, RouterOutlet} from '@angular/router';
import {AuthService} from './auth/service/auth-service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Frontend');
  constructor(private router: Router, protected authService: AuthService) {
    this.authService.restoreSession().subscribe();
  }

  protected navLoading = signal(false);
  protected navError = signal('');

  protected onLogout(): void {
    this.navLoading.set(true);
    this.navError.set('');

    this.authService.logout().subscribe({
      next: () => {
        this.navLoading.set(false);
        this.router.navigate(['/login']);
      },
      error: (err) => {
        const msg = err?.error?.message ?? 'Logout failed.';
        this.navError.set(msg);
        this.navLoading.set(false);
      }
    });
  }
}
