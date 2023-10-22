/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GeoServerService } from './geo-server.service';

describe('Service: GeoServer', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GeoServerService]
    });
  });

  it('should ...', inject([GeoServerService], (service: GeoServerService) => {
    expect(service).toBeTruthy();
  }));
});
