import { HttpErrorResponse, HttpEvent, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, finalize, tap, throwError } from 'rxjs';
import { IMessage } from '../interfaces/message.interface';
import { NotificationService } from '../services/notification.service';

let messages: IMessage[] = [];

export const notificationInterceptor: HttpInterceptorFn = (req, next) => {
  const notificationService = inject(NotificationService);
  const addMessages = (responseMessages: IMessage[]) => {
    if (Array.isArray(responseMessages)) {
      messages.push(...responseMessages);
    }
  }

  const notifyMessages = () => {
    for (const message of messages) {
      notificationService.show(message);
    }

    messages = [];
  }

  return next(req).pipe(
    tap((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        addMessages(event?.body?.messages);
      }
    }),
    catchError((error: HttpErrorResponse) => {
      addMessages(error?.error?.messages);
      return throwError(() => error);
    }),
    finalize(() => notifyMessages())
  );
}
