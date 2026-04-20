import {Component, signal} from '@angular/core';
import {Router} from '@angular/router';
import {AuthService} from '../../service/auth-service';
import {LoginDto} from '../../dto/login-dto';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-login-page',
  imports: [
    FormsModule
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  constructor(public router: Router, private authService: AuthService) { }

  loading = signal(false);
  error = signal('');
  success = signal('');

  dto: LoginDto = {
    email: '',
    password: '',
  }

  onSubmit(): void {
    if (!this.dto.email || !this.dto.password) {
      this.error.set('Email and password are required');
      return;
    }

    this.loading.set(true);
    this.error.set('');
    this.success.set('');

    this.authService.login(this.dto).subscribe({
      next: (res) => {
        this.success.set(res.message ?? 'Logged in.');
        this.loading.set(false);
        this.router.navigate(['/contacts']);
      },
      error: (err) => {
        const msg = err?.error?.message ?? 'An error occurred during login.';
        this.error.set(msg);
        this.loading.set(false);
      }
    });
  }
}
