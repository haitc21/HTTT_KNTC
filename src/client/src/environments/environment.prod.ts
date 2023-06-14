import { Environment } from '@abp/ng.core';

const baseUrl = 'http://10.4.1.9:4201';


export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'KNTC',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://10.4.1.9:5101/',
    clientId: 'KNTC_App',
    scope: 'offline_access KNTC',
    requireHttps: false,
  },
  apis: {
    default: {
      url: 'http://10.4.1.9:5102',
      rootNamespace: 'KNTC',
    },
    geoserver: {
      url: 'http://10.4.1.9:8080',
      rootNamespace: 'KNTC',
    },
  },
} as Environment;
