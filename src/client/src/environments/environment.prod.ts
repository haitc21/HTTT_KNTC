import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4201';


export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'KNTC',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:5101/',
    // redirectUri: baseUrl,
    clientId: 'KNTC_App',
    // responseType: 'code',
    scope: 'offline_access KNTC',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:5102',
      rootNamespace: 'KNTC',
    },
    geoserver: {
      url: 'http://localhost:8080',
      rootNamespace: 'KNTC',
    },
  },
} as Environment;
