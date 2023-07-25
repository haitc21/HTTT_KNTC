import { Injectable } from '@angular/core';
import { AllSysConfigCacheItem, SysConfigService } from '@proxy/sys-configs';
import { Observable, tap } from 'rxjs';
import { SysConfigConsts } from '../constants/sys-config.consts';
@Injectable({
  providedIn: 'root',
})
export class GetSysConfigService {
  // Biến để kiểm tra xem có thể gọi API hay không
  private canCallApi = true;

  constructor(private sysConfigService: SysConfigService) {}

  getAll(): Observable<AllSysConfigCacheItem> {
    const cacheKey = SysConfigConsts.Prefix;
    const storedConfig = localStorage.getItem(cacheKey);

    if (storedConfig) {
      const { value, expiry } = JSON.parse(storedConfig);
      const currentTime = new Date().getTime();

      if (currentTime < expiry) {
        return new Observable(observer => {
          observer.next(value);
          observer.complete();
        });
      } else {
        localStorage.removeItem(cacheKey);
      }
    }

    // Kiểm tra xem có thể gọi API hay không
    if (this.canCallApi) {
      // Gọi API
      this.canCallApi = false;
      return this.sysConfigService.getAllConfigs().pipe(
        tap(data => {
          const expiryTime = new Date().getTime() + 10 * 60 * 1000; // 10 minutes
          const configToStore = {
            value: data,
            expiry: expiryTime,
          };
          localStorage.setItem(cacheKey, JSON.stringify(configToStore));

          // Sau 10 giây, cho phép gọi API tiếp theo
          setTimeout(() => {
            this.canCallApi = true;
          }, 10000);
        })
      );
    } else {
      // Nếu không thể gọi API, trả về một Observable rỗng
      return new Observable(observer => {
        observer.complete();
      });
    }
  }
}
