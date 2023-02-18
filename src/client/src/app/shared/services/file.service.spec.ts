import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  apiName = 'Default';

  getAvatar = (userId: string) =>
    this.restService.request<any, any>(
      {
        method: 'GET',
        url: `/api/app/users/avatar/${userId}`,
      },
      { apiName: this.apiName }
    );

  uploadAvatar = (file: File) =>
    this.restService.request<any, string>(
      {
        method: 'POST',
        responseType: 'text',
        url: '/api/app/users/upload-avatar',
        body: this.generateFormData(file),
      },
      { apiName: this.apiName }
    );
  private generateFormData(file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return formData;
  }

  constructor(private restService: RestService) {}
}
