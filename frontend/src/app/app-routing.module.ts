import { Injectable, NgModule } from '@angular/core';
import { CanActivate, Router, RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './core/components/auth/auth.component';
import { TasksComponent } from './features/tasks/tasks.component';
import { Observable } from 'rxjs';
import { AuthService } from './core/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private _authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    return !!this._authService.isAuthenticated;
  }
}

const routes: Routes = [
  { path: '', component: TasksComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'auth/:operation', component: AuthComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
