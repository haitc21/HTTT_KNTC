import { Environment } from '@abp/ng.core';

const baseUrl = 'https://*.KNTC.com:4201';


export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'KNTC',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://*.KNTC.com:5101/',
    clientId: 'KNTC_App',
    scope: 'offline_access KNTC',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://*.KNTC.com:5102',
      rootNamespace: 'KNTC',
    },
    geoserver: {
      url: 'https://*.KNTC.com:8080',
      rootNamespace: 'KNTC_GÉOERVER',
    },
  },
} as Environment;
