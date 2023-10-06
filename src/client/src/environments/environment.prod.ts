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
    issuer: 'https://gqkntc.tnmtthainguyen.gov.vn:5101/',
    redirectUri: baseUrl,
    clientId: 'KNTC_App',
    responseType: 'code',
    scope: 'offline_access KNTC',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://gqkntc.tnmtthainguyen.gov.vn:5101',
      rootNamespace: 'KNTC',
    },
  },
} as Environment;
