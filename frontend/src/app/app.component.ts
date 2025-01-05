import { Component } from '@angular/core';
import { AuthService } from './core/auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  constructor(
    private readonly _authService: AuthService
  ) {
  }

  get isAuthenticated() {
    return this._authService.isAuthenticated;
  }
}
