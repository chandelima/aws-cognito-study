import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UnsubscriberService } from '../../services/unsubscriber.service';
import { AuthService } from '../../auth/services/auth.service';
import { combineLatest } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-auth',
  template: '',
})
export class AuthComponent implements OnInit {

  constructor(
    private readonly _authService: AuthService,
    private readonly _route: ActivatedRoute,
    private readonly _unsubscriberService: UnsubscriberService,
    private readonly _location: Location
  ) {}

  ngOnInit(): void {
    this.subscribeRouteParams();
  }

  subscribeRouteParams() {
    const routeParams$ = this._route.paramMap;
    const queryParams$ = this._route.queryParamMap;

    combineLatest([routeParams$, queryParams$])
      .pipe(this._unsubscriberService.takeUntilDestroy)
      .subscribe(([routeParamsMap, queryParamsMap]: any) => {
        const operation = routeParamsMap?.params?.operation;
        const queryParams = queryParamsMap.params;

        switch (operation) {
          case "login":
            this.getLoginUrl();
            break;

          case "callback":
            const code = queryParams["code"];
            this.completeLogin(code);
            break;

          case "logout":
            this.logout();
        }
      })
  }

  getLoginUrl() {
    this._authService.getLoginUrl().subscribe(response => {
      if (response?.data?.value) {
        this._location.go('');
        window.location.href = response.data.value;
      }
    })
  }

  completeLogin(authCode: string) {
    this._authService.completeLogin(authCode).subscribe();
  }

  logout() {
    this._authService.logout().subscribe();
  }
}


