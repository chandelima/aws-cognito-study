import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

import { NgxSpinnerModule } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { Toast } from 'primeng/toast';
import { CoreComponent } from './components/core/core.component';
import { notificationInterceptor } from './interceptors/notification.interceptor';
import { spinnerInterceptor } from './interceptors/spinner.interceptor';
import { providePrimeNG } from 'primeng/config';
import { themePreset } from './styles/theme-preset';
import { authInterceptor } from './interceptors/auth.interceptor';


@NgModule({
  declarations: [ CoreComponent ],
  imports: [
    CommonModule,
    NgxSpinnerModule,
    Toast
  ],
  exports: [ CoreComponent ],
  providers: [
    MessageService,
    provideAnimationsAsync(),
        providePrimeNG({
            theme: {
              preset: themePreset,
              options: {
                darkModeSelector: 'none',
              },
            },
        })
  ]
})
export class CoreModule {
  static interceptors = [ notificationInterceptor, spinnerInterceptor, authInterceptor];
}
