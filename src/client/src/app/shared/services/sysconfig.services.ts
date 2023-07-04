import { Injectable } from '@angular/core';
import { SysConfigService } from '@proxy/sys-configs';
import { Observable, tap } from 'rxjs';
import { SysConfigConsts } from '../constants/sys-config.consts';

@Injectable({
  providedIn: 'root',
})
export class GetSysConfigService {
  constructor(private sysConfigService: SysConfigService) {}

  getSysConfig(name: string): Observable<any> {
    const cacheKey = `${SysConfigConsts.Prefix}${name}`
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
    return this.sysConfigService.getByName(name).pipe(
      tap(data => {
        const expiryTime = new Date().getTime() + 10 * 60 * 1000; // 10 minutes
        const configToStore = {
          value: data,
          expiry: expiryTime,
        };
        localStorage.setItem(cacheKey, JSON.stringify(configToStore));
      })
    );
  }
}
