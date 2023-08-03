import { Environment } from '@abp/ng.core';

const baseUrl = 'http://gqkntc.tnmtthainguyen.gov.vn:4201';


export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'KNTC',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://gqkntc.tnmtthainguyen.gov.vn:5101/',
    redirectUri: baseUrl,
    clientId: 'KNTC_App',
    responseType: 'code',
    scope: 'offline_access KNTC',
    requireHttps: false,
  },
  apis: {
    default: {
      url: 'http://gqkntc.tnmtthainguyen.gov.vn:5102',
      rootNamespace: 'KNTC',
    },
    geoserver: {
      url: 'http://gqkntc.tnmtthainguyen.gov.vn:8080',
      rootNamespace: 'KNTC',
    },
  },
} as Environment;
