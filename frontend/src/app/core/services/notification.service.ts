import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { IMessage, IMessageType } from '../interfaces/message.interface';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {

  constructor(
    private messageService: MessageService
  ) { }

  show(message: IMessage) {
    const mapValue = this.mapPrimeNgSeverity(message.type);
    const severity = mapValue.key;
    const summary = mapValue.title;

    this.messageService.add({
      severity, summary,
      detail: message.message
    });
  }

  private mapPrimeNgSeverity(type: IMessageType): { key: string, title: string } {
    const map = {
      'Success':      { key: 'success', title: 'Sucesso' },
      'Information':  { key: 'info', title: 'Informação' },
      'Error':        { key: 'error', title: 'Erro' }
    };

    return map[type];
  }
}
