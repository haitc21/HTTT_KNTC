import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable()
export class NotificationService {
  constructor(private messageService: MessageService) {}

  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Thành công', detail: message });
  }

  showError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Lỗi', detail: message, life: 5000 });
  }

  showWarn(message: string) {
    this.messageService.add({ severity: 'warn', summary: 'Cảnh báo', detail: message });
  }
  showInfo(message: string) {
    this.messageService.add({ severity: 'info', summary: 'Thoong tin', detail: message });
  }
}
