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
    issuer: 'http://localhost:5101/',
    clientId: 'KNTC_App',
    scope: 'offline_access KNTC',
    requireHttps: false,
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
