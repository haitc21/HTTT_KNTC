import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeoServerService {

  private geoserverUrl = 'http://gqkntc.tnmtthainguyen.gov.vn:8080/geoserver/kntc/wfs?request=getCapabilities';

  constructor(private http: HttpClient) { }

  getFeatureTypes(): Observable<string> {
    return this.http.get(this.geoserverUrl, { responseType: 'text' });
  }

}
