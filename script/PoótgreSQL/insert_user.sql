-- TRUNCATE
Truncate table "AbpOrganizationUnitRoles" CASCADE;
Truncate TABLE public."AbpRoleClaims" CASCADE;
Truncate TABLE public."AbpUserRoles" CASCADE;
Truncate TABLE public."AbpUserClaims" CASCADE;
Truncate TABLE public."AbpUserOrganizationUnits" CASCADE;
Truncate TABLE public."AbpUserTokens" CASCADE;
Truncate TABLE public."AppUserInfos" CASCADE;
Truncate TABLE public."AbpRoles" CASCADE;
Truncate TABLE public."AbpUsers" CASCADE;
Truncate TABLE public."AbpPermissionGrants" CASCADE;

-- INSERT 

INSERT INTO public."AbpUsers" VALUES ('3a0befb4-53a8-bca8-919a-2e4580fe98e6', NULL, 'tiepdan', 'TIEPDAN', 'tiếp dân', 'Bộ phận', 'tiepdan@tnmtthainguyen.gov.vn', 'TIEPDAN@TNMTTHAINGUYEN.GOV.VN', false, 'AQAAAAEAACcQAAAAEAX28GNE4mp1A7lClYsRxmcEsebj1iBHMJUXs8RMyVgErsoTkCbzWsdAeGUkpX7Vag==', 'UIYG4676TXGLDDV2CYMC7CGBKVXHYZ7S', false, '0900909090', false, true, false, NULL, false, 0, false, 0, NULL,  '{}', '06ffb506e6694480a2aa1f3820f2d81c', '2023-06-21 17:37:04.010673+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-06-21 17:42:24.573573+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', false, NULL, NULL);
INSERT INTO public."AbpUsers" VALUES ('3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, 'admin', 'ADMIN', 'Hệ thống', 'Quản trị', 'admin@gmail.com', 'ADMIN@GMAIL.COM', false, 'AQAAAAEAACcQAAAAEBwvj285mZNCosa7QimL4mC8ftklySfDNOd1u0ngIY9bGb686JrXLNnYM3DiUItlUA==', 'DMTS5R3W4WWJO3NZFHOVYHW67D66IPOA', false, '0900909090', false, true, false, NULL, true, 0, false, 0, NULL,  '{}', '51cca8d77d5c4423a874c8cc77e28049', '2023-06-11 23:51:55.527735+07', NULL, '2023-07-07 09:10:42.100927+07', NULL, false, NULL, NULL);
INSERT INTO public."AbpUsers" VALUES ('3a0befbf-bcc7-edb2-696f-fad939792b0d', NULL, 'lanhdao', 'LANHDAO', 'Văn phòng', 'Lãnh đạo', 'lanhdao@tnmtthainguyen.gov.vn', 'LANHDAO@TNMTTHAINGUYEN.GOV.VN', false, 'AQAAAAEAACcQAAAAEKr/4LLPvXOWvoPLzpKMcS+Gtwiw21Lm+0W9GU+Eo01ok0Ij5xAm1OkKv0Fa9Mm27g==', 'KBLB4J2H4SJLFAP4EGRGMXQORRMRF7AS', false, '0900909090', false, true, false, NULL, false, 0, false, 0, NULL,  '{}', '409687d68cb44ae3b16bbe6a2fb417af', '2023-06-21 17:49:31.62164+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-06-21 17:49:45.59525+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', false, NULL, NULL);
INSERT INTO public."AbpUsers" VALUES ('3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, 'Ninh', 'NINH', 'Ninh', 'Duong ', 'bacninh157@gmail.com', 'BACNINH157@GMAIL.COM', false, 'AQAAAAEAACcQAAAAEIkztX+lFyHSK/soAzTwLp3ae70sHGd780CbjbeYRdKY0hPHL7RAhVsN5rYE6PJtgg==', 'ETF2S6COWE6WEMZR3X2EWE4DCHVCRJGU', false, '+84918173686', false, true, false, NULL, false, 0, false, 0, NULL,  '{}', '5c7ba89008684a1e96e602ed4ab0eb54', '2023-07-26 10:55:10.564569+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-26 11:01:20.551212+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', false, NULL, NULL);
INSERT INTO public."AbpUsers" VALUES ('3a0bf32a-9df9-6533-43f5-6c2a0ed11ea6', NULL, 'ptthuyen', 'PTTHUYEN', 'Huyền', 'Phan Thị Thanh ', 'ptthuyen@vnua.edu.vn', 'PTTHUYEN@VNUA.EDU.VN', false, 'AQAAAAEAACcQAAAAELZAgOw8eFfG0ETxMuhDe5djDUL2yjBlNk3aerOUmlrn3sQj/PQW7zT56eDDhPsh/g==', 'YSHROFJXQH7VLUU3DGYC4DLX73576SOR', false, '0988083673', false, true, false, NULL, false, 0, false, 0, NULL,  '{}', '21893c4d8dca4fb9af4f3a9e270fe8e3', '2023-06-22 09:45:07.773979+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-06-24 16:36:19.952405+07', NULL, false, NULL, NULL);


INSERT INTO public."AbpRoles" VALUES ('3a0bbd8b-f0e1-abdf-b3aa-bc1428e02e65', NULL, 'admin', 'ADMIN', false, true, true, 0, '{"Description":"Qu\u1EA3n tr\u1ECB h\u1EC7 th\u1ED1ng"}', '061cc8351ead42fb83bb84a4277e0cdc');
INSERT INTO public."AbpRoles" VALUES ('3a0befb5-ba1b-deba-cebd-a7f7c0c43ebe', NULL, 'Lãnh đạo', 'LÃNH ĐẠO', false, false, true, 0, '{"Description":"L\u00E3nh \u0111\u1EA1o th\u1EE5 l\u00FD"}', 'd5e38fee3d374595a174e06d1aa45646');
INSERT INTO public."AbpRoles" VALUES ('3a0bbfa3-f1fd-256e-c4de-ad5340d1ce5a', NULL, 'Bộ phận tiếp dân', 'BỘ PHẬN TIẾP DÂN', false, false, true, 0, '{"Description":"L\u00E0 b\u1ED9 ph\u1EADn ti\u1EBFp d\u00E2n"}', '0fc138a7cd4a4772ba55a7065b09896c');
INSERT INTO public."AbpRoles" VALUES ('3a0ca27f-f0ba-786e-ef0a-5eeac188fd5e', NULL, 'Ninh', 'NINH', false, false, true, 0, '{"Description":"Cap nhat"}', '8b779a73cda34819ad94042f5b4b304f');



INSERT INTO public."AppUserInfos" VALUES ('3a0befb4-5686-d3b6-5645-c6affbc7992f', '3a0befb4-53a8-bca8-919a-2e4580fe98e6', '1991-01-16 00:00:00+07');
INSERT INTO public."AppUserInfos" VALUES ('3a0befbf-bd19-0e8b-df39-932ec8f1dbb6', '3a0befbf-bcc7-edb2-696f-fad939792b0d', '-infinity');
INSERT INTO public."AppUserInfos" VALUES ('3a0bbd8b-fb89-bf04-fc05-de054e26f7b2', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '1990-05-30 00:00:00+07');
INSERT INTO public."AppUserInfos" VALUES ('3a0bf32a-9f21-11f6-5237-abcdd57b410d', '3a0bf32a-9df9-6533-43f5-6c2a0ed11ea6', '-infinity');
INSERT INTO public."AppUserInfos" VALUES ('3a0ca282-f7b8-1b5f-7cd8-a749e3a31d13', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-25 00:00:00+07');



INSERT INTO public."AbpUserRoles" VALUES ('3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '3a0bbd8b-f0e1-abdf-b3aa-bc1428e02e65', NULL);
INSERT INTO public."AbpUserRoles" VALUES ('3a0befb4-53a8-bca8-919a-2e4580fe98e6', '3a0bbfa3-f1fd-256e-c4de-ad5340d1ce5a', NULL);
INSERT INTO public."AbpUserRoles" VALUES ('3a0befbf-bcc7-edb2-696f-fad939792b0d', '3a0befb5-ba1b-deba-cebd-a7f7c0c43ebe', NULL);
INSERT INTO public."AbpUserRoles" VALUES ('3a0ca282-f6d3-5af7-de36-8d7b987f275b', '3a0bbd8b-f0e1-abdf-b3aa-bc1428e02e65', NULL);



INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3b9-13fc-2947-b724729bcd6f', NULL, 'AbpIdentity.Roles', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3ce-e170-3cbb-e9459b7f81cc', NULL, 'AbpIdentity.Roles.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d2-59e1-0ddd-7d437014ed06', NULL, 'AbpIdentity.Roles.Update', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d3-098c-98ad-48f486c8f400', NULL, 'AbpIdentity.Users.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d3-5855-c8c3-6c610f913c35', NULL, 'AbpIdentity.Users', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d3-8074-c9b4-217ef6b6a1e3', NULL, 'AbpIdentity.Users.Update', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d3-8108-bb91-31f5be7d72b7', NULL, 'AbpIdentity.Roles.ManagePermissions', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d3-8233-9aa4-d6e72745aa67', NULL, 'AbpIdentity.Roles.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d4-2db4-efae-5cba040ff5a4', NULL, 'FeatureManagement.ManageHostFeatures', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d4-2db9-0734-4ff780dc3415', NULL, 'SettingManagement.Emailing', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d4-53a5-83d7-d63aeead6eec', NULL, 'AbpIdentity.Users.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d4-b98e-c8f7-93e93d59ea5f', NULL, 'AbpIdentity.Users.ManagePermissions', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d5-1830-5d31-9ca063f04c1f', NULL, 'Complains.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d5-88aa-de1c-ea791ddd0eb4', NULL, 'Complains', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d5-cc6b-e5f5-40da9b1e6bb3', NULL, 'SettingManagement.Emailing.Test', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d5-dcf3-7708-5ab5681575c3', NULL, 'Complains.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d6-7fe2-692d-c93baaa73df3', NULL, 'Denounces.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d6-89e8-f450-a4da87e2391e', NULL, 'Complains.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d6-aacd-dea0-6be2f2aa7d8c', NULL, 'Denounces.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d6-fb81-8ecd-a24530137d4e', NULL, 'Denounces', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d7-45d8-bdf4-540f22a10f5e', NULL, 'Denounces.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d7-9bb0-60fe-f436c333b322', NULL, 'DocumentType', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d7-acad-ee37-21d2667999da', NULL, 'DocumentType.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3d7-f2da-713a-541cccb046ff', NULL, 'DocumentType.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e4-5d86-dfcc-1e4df5c78a50', NULL, 'DocumentType.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e4-8ac4-f065-00a3453a85f5', NULL, 'LandType', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e5-0d35-5d80-0ee4643a9a55', NULL, 'LandType.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e5-20cb-3020-8ee746366fa6', NULL, 'LandType.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e5-6fbc-4688-ce612a484429', NULL, 'LandType.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e5-e2df-0e5d-0c65d9fb03a5', NULL, 'Unit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e6-cc44-1d18-19961525daec', NULL, 'Unit.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e6-d5f3-f04e-dd3f585c94b2', NULL, 'UnitType', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e6-f60f-f828-3ec253f3307b', NULL, 'Unit.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e6-f9d7-571b-bc3326e7b098', NULL, 'Unit.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e7-5d26-ab61-41680fa823f8', NULL, 'UnitType.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e7-63a4-084b-a794e1a2db6b', NULL, 'UnitType.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bbd8b-f3e7-c6fb-7df7-2d1f185a7e36', NULL, 'UnitType.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d000-da07-c5a2-07e9c28d4722', NULL, 'Complains', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d002-df08-2c94-034bb90c79f6', NULL, 'Complains.Create', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d004-2166-27af-072997d1faac', NULL, 'Complains.Edit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d006-c46f-78eb-bcbdfeea0286', NULL, 'Complains.Delete', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d008-ce69-8029-384e3657a882', NULL, 'Denounces', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d00a-1122-6682-7d3c74fbf86e', NULL, 'Denounces.Create', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d00d-2aad-f81c-c9359515ba85', NULL, 'Denounces.Edit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d00f-3793-49fc-f433db8aacaa', NULL, 'Denounces.Delete', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d011-faf0-6e35-9f86e15f0703', NULL, 'DocumentType', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d013-42be-0023-0d5586de1ace', NULL, 'DocumentType.Create', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d015-20d9-022f-e9d7084c06b7', NULL, 'DocumentType.Edit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d017-9cd6-f446-656d725abba2', NULL, 'DocumentType.Delete', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d019-0f39-b711-8a5819e011f3', NULL, 'LandType', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d01b-8c36-a422-e16d7db4d018', NULL, 'LandType.Create', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d01d-e7f1-f94c-e404a9463984', NULL, 'LandType.Edit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d01f-7529-f965-2ae761a8ac44', NULL, 'LandType.Delete', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d021-d7f8-6c4e-1170c4f4c9c6', NULL, 'Unit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d023-6979-dd18-27cabe4a88e6', NULL, 'Unit.Create', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d025-24e0-c64c-277e183e2a08', NULL, 'Unit.Edit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d028-33af-bee8-5bf73aa6fcbe', NULL, 'Unit.Delete', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c8f-a84a-63c3-4290880b86d8', NULL, 'Complains', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c93-0d3f-d370-e80ffbfa2390', NULL, 'Complains.Create', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c97-808b-6771-834ef7642e8e', NULL, 'Complains.Edit', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c99-eb1f-3c25-3b7bf620f858', NULL, 'Complains.Delete', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c9b-a99e-3276-b1b64a3410ed', NULL, 'Denounces', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c9d-2706-70fa-e39a79545279', NULL, 'Denounces.Create', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2c9f-0ae7-0a2e-23a69b3523c4', NULL, 'Denounces.Edit', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2ca1-3f6b-bfd4-bcec17b2c5c6', NULL, 'Denounces.Delete', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2ca3-07dc-dca9-a433a66846d8', NULL, 'DocumentType', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2ca5-4e34-00c6-8d834a6a51c2', NULL, 'DocumentType.Create', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2ca7-f8d9-6502-2aa295ad00c1', NULL, 'DocumentType.Edit', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-2ca9-7103-2a08-e5b3015aee12', NULL, 'DocumentType.Delete', 'R', 'Bộ phận tiếp dân');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d02a-cbdd-0d94-b046572d437a', NULL, 'UnitType', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d02c-7178-ea48-656f3a01edf3', NULL, 'UnitType.Create', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d02e-4cef-19ea-4ba89c129b01', NULL, 'UnitType.Edit', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0befba-d030-9cc0-3458-3e2fcc9dee52', NULL, 'UnitType.Delete', 'R', 'Lãnh đạo');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-56e6-bad2-ccaa-da5c00d91006', NULL, 'DocumentTypes', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5729-1d3e-149f-b1bafda3b7de', NULL, 'DocumentTypes.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5730-c243-30f3-7e96f6561f92', NULL, 'DocumentTypes.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5733-db47-8904-5d549a9e187b', NULL, 'DocumentTypes.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5735-5252-e53b-0395b5adf0a4', NULL, 'LandTypes', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5737-5562-1159-9b193c8b18e4', NULL, 'LandTypes.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5739-5928-23f0-c585e9a1013d', NULL, 'LandTypes.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-573b-f42b-9b54-8b8fdf29ed60', NULL, 'LandTypes.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-573d-ca0b-733a-1bcdceddd3fc', NULL, 'Units', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-573f-196c-ff97-0374391384b6', NULL, 'Units.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5742-ff41-e3b2-2764a519c3d1', NULL, 'Units.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5744-df18-8507-d7c0710f27b5', NULL, 'Units.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5746-e943-dba2-b9d533332c1d', NULL, 'UnitTypes', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5748-9cff-449f-b05d94a51344', NULL, 'UnitTypes.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-574a-bccd-68a8-b63551f25a2b', NULL, 'UnitTypes.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-574d-fb34-0866-863ce6247de0', NULL, 'UnitTypes.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-574f-d855-781e-8eba7075ecb4', NULL, 'GeoServesrs', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5751-3a56-c34e-d537f45a372c', NULL, 'Configs', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5753-5fae-c6d1-8b797a382115', NULL, 'Configs.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5757-e8f1-92bc-943c86c9b41f', NULL, 'Configs.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0bf308-5759-a5c9-b935-d3c4c6a9713d', NULL, 'Configs.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0c3257-5a2c-e7b2-f4f8-ca43091e9628', NULL, 'SysConfigs', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0c3257-5a70-25db-cf51-731ee7191d0e', NULL, 'SysConfigs.Create', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0c3257-5a75-1ca6-ef7c-a65099c71317', NULL, 'SysConfigs.Delete', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0c3257-5a75-8888-6279-75e955b27eca', NULL, 'SysConfigs.Edit', 'R', 'admin');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4783-7233-a1eb-d67bae749dce', NULL, 'AbpIdentity.Roles', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4788-7702-af40-97f91af6a22a', NULL, 'AbpIdentity.Roles.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-478a-6ed9-c816-ec33afcaf247', NULL, 'AbpIdentity.Roles.Update', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-478c-e907-956d-6dc72a309d64', NULL, 'AbpIdentity.Roles.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-478e-22c8-5ace-26b0fa88315d', NULL, 'AbpIdentity.Roles.ManagePermissions', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4791-4633-b2a1-8b635abf6b1a', NULL, 'AbpIdentity.Users', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4793-ecac-9dae-4d21196ce44c', NULL, 'AbpIdentity.Users.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4795-d346-c87f-9817c34237cd', NULL, 'AbpIdentity.Users.Update', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4797-1a46-5a07-0d39a712c0ac', NULL, 'AbpIdentity.Users.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-4799-1aab-daf0-5a91ae1e538a', NULL, 'AbpIdentity.Users.ManagePermissions', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-479b-d82a-bc6f-36bb18e01e3a', NULL, 'Complains', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-479d-c057-e464-6c57906bfdec', NULL, 'Complains.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47a0-ddfa-2e70-994d0f8f24e1', NULL, 'Complains.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47a2-9c4b-81ed-b82a061e0e1d', NULL, 'Complains.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47a4-89d3-cf7a-c36d9c2d53c2', NULL, 'Denounces', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47a6-5dd6-3cbf-88e2af95b5e7', NULL, 'Denounces.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47a8-2666-b270-335b1013aa70', NULL, 'Denounces.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47aa-6291-9250-8a084c9322f6', NULL, 'Denounces.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47ac-ec68-fe51-e23d9bb527a7', NULL, 'DocumentTypes', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47af-d0c5-3678-2f23c0dfc6da', NULL, 'DocumentTypes.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47b1-e430-1ad3-496d57f3c823', NULL, 'DocumentTypes.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47b3-a7b4-8944-b25636a1b892', NULL, 'DocumentTypes.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47b5-0802-835f-8f98a4d8937c', NULL, 'LandTypes', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47b7-6951-6386-d3ce6c029253', NULL, 'LandTypes.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47b9-f1e7-aa96-2b95f7ac8019', NULL, 'LandTypes.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47bc-040c-9414-9a1ea41799ca', NULL, 'LandTypes.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47be-601b-dbb0-cd4f6fe7f240', NULL, 'Units', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47c0-d2b5-55bc-a4145a83f193', NULL, 'Units.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47c2-7a04-094e-9c81620dde57', NULL, 'Units.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47c4-ed36-8ab9-70b43d97b546', NULL, 'Units.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47c6-d097-4ad6-b905ee6846e6', NULL, 'UnitTypes', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47c9-00a0-a6ef-e973fbf041a4', NULL, 'UnitTypes.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47cb-fd90-cdf1-58794ade6d68', NULL, 'UnitTypes.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47cd-611e-aab3-e5b6d940e723', NULL, 'UnitTypes.Delete', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47cf-980a-5788-96a7a3281651', NULL, 'GeoServesrs', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47d1-27ba-655e-8d8418408fb8', NULL, 'SysConfigs', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47d3-3818-707f-b6d912b06760', NULL, 'SysConfigs.Create', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47d6-ad6b-dba5-7fbef928c35a', NULL, 'SysConfigs.Edit', 'R', 'Ninh');
INSERT INTO public."AbpPermissionGrants" VALUES ('3a0ca280-47d8-0442-cf34-0d39fa6059f8', NULL, 'SysConfigs.Delete', 'R', 'Ninh');




INSERT INTO "KNTC"."LandTypes" VALUES (5, 'CHN', 'Đất trồng cây hàng năm', NULL, 9, 1, NULL, NULL, '2023-06-11 23:51:58.602627+07', NULL, NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (6, 'DSX', 'Đất sản xuất, kinh doanh phi nông nghiệp', NULL, 6, 1, NULL, NULL, '2023-06-11 23:51:58.602627+07', NULL, NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (7, 'DNK', 'Đất phi nông nghiệp khác', NULL, 7, 1, NULL, NULL, '2023-06-11 23:51:58.602627+07', NULL, NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (8, 'DTS', 'Đất nuôi trồng thủy sản', NULL, 8, 1, NULL, NULL, '2023-06-11 23:51:58.602627+07', NULL, NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (9, 'DTC', 'Đất trồng cây lâu năm', NULL, 5, 1, NULL, NULL, '2023-06-11 23:51:58.602627+07', NULL, NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (10, 'DLN', 'Đất lâm nghiệp', 'Đất lâm nghiệp', 11, 1, '{}', '788b996ea0974fa6bd8f1f4b693adef0', '2023-07-19 13:55:49.903002+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (11, 'ON111', 'Đất ở', 'Đất mới tạo xong không lưu được', 3, 1, '{}', 'd382d58febc74396bb46f45ff40b6a21', '2023-07-19 16:02:01.590332+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."LandTypes" VALUES (12, 'ON', 'Đất ở nhé', '0 lưu được à', 3, 1, '{}', 'c64c51613a0a45b39403cd360476cc5a', '2023-07-19 16:02:38.113149+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);



INSERT INTO "KNTC"."Complains" VALUES ('10aa4d95-be8b-4ccb-ca27-3a0ab1963c32', '1929/KN-90', 3, 'Khiếu nại về việc ngăn sông làm mất nguồn nước nuôi trồng thủy sản', 'Phan Văn Em', '12121212323', '2000-02-04 00:00:00+07', '0909123123', 'em@e.com', 'Thị trấn Quân Chu', 'Thị trấn Quân Chu', 24, 706, 8836, '2013-04-10 23:03:00+07', '2017-04-19 23:03:00+07', '<p>Khiếu kiện v.v ngăn sông làm mất nguồn nước nuôi trồng thủy sản</p>', 'Thanh tra huyện Phổ Yên', '124', '532B', 1000, 4, 'ds đấ', 24, 706, 8836, '21.75653430267445, 105.71996280275967', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.719613,21.756917],[105.72029,21.756937],[105.72074,21.756309],[105.719785,21.75601],[105.719227,21.756498],[105.719613,21.756917]]]}}', '', 1, NULL, NULL, NULL, NULL, 3, NULL, NULL, NULL, NULL, NULL, 3, 3, false, '{}', 'b3bee64f287440f9a883978ef30a595c', '2023-04-20 23:04:59+07', '6995b140-27c7-e4a4-c857-3a0a5a041363', '2023-07-17 11:32:46.156315+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Complains" VALUES ('e133e22b-51e3-a5af-a5db-3a0ab18a3a37', '123/CVBD', 2, 'Khiếu nại về quyết định xử phạt Công ty chế thực phẩm Bắc Hà', 'Lê Thế Thái', '1232132111', '1956-11-11 00:00:00+07', '0902392031', 'th@gmail.com', 'xã Dân Tiến, huyện Võ Nhai', 'xã Dân Tiến, huyện Võ Nhai', 24, 517, 1561, '2021-04-18 22:49:00+07', '2021-04-29 22:49:23+07', '<p>Khiếu nại về quyết định xử phạt hành vi làm ô nhiễm môi trường không khí</p>', 'Thanh tra huyện Võ Nhai', '1212', '23213CV', 500, 2, 'Xã La Hiên', 24, 517, 1183, '21.779959142795324, 105.94092164136839', '{"type":"Feature","properties":{},"geometry":{"type":"LineString","coordinates":[[105.939806,21.782191],[105.945813,21.785538],[105.963574,21.771271],[105.968808,21.776213],[105.972669,21.772148],[105.958512,21.761387],[105.953792,21.761307],[105.950961,21.764655],[105.948215,21.772307],[105.940922,21.779959]]}}', 'Xử phạt ô nhiễm khói bụi ', 1, '2023-04-19 00:00:00+07', '2023-05-18 00:00:00+07', 'Chủ tịch UBND huyện Võ Nhai', '2321/QĐ-CT', 2, NULL, NULL, NULL, NULL, NULL, NULL, 2, true, '{}', '7dfe1ffa250f49acbd7ae08d1ed24d73', '2023-04-20 22:51:53+07', '6995b140-27c7-e4a4-c857-3a0a5a041363', '2023-07-17 14:43:52.922083+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Complains" VALUES ('09d22d49-9a03-09fd-87c8-3a0ab07f37e2', '12321/HS-2321', 4, 'Khiếu nại về quyết định xử phạt khai thác cát trái pháp luật', 'Lê Văn Bạch', '1212321321312', '1990-04-05 00:00:00+07', '0909090909', 'bach@co.uk', 'Thôn A, xã Đào Xá, huyện Phú Bình, tỉnh Thái Nguyên', 'Thôn A, xã Đào Xá, huyện Phú Bình, tỉnh Thái Nguyên', 24, 368, 2288, '2023-04-05 17:58:04+07', '2023-04-27 17:58:09+07', '<p>Khiếu nại về quyết định xử phạt khai thác cát trái pháp luật</p>', 'Thanh tra huyện Phú Bình', '2312', '3213v', 123, 4, 'Xã Đào Xá, huyện Phú Bình', 24, 368, 2288, '21.737401011620843, 106.10432413326946', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[106.103213,21.735321],[106.103213,21.738032],[106.109263,21.738032],[106.109263,21.735321],[106.103213,21.735321]]]}}', 'Ông Lê Văn Bạch gửi đơn', NULL, '2022-08-08 00:00:00+07', '2022-09-08 00:00:00+07', 'Chủ tịch UBND huyện Phú Bình', '2321/QĐ-CT', 3, NULL, NULL, NULL, NULL, NULL, 0, 0, true, '{}', '5d353a96b5b54e10b5e6f2c1028f5844', '2023-04-20 18:00:16+07', '6995b140-27c7-e4a4-c857-3a0a5a041363', '2023-07-17 14:55:38.921817+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Complains" VALUES ('3a0c84e0-2420-55cb-f730-26afe1cb2d37', 'KNDD.22.06.2023.34444', 1, 'Khiếu nại cấp GCN', 'Đoàn Văn Hùng (Đoàn Thế Hùng)', '091038544', '1970-11-15 00:00:00+07', '0975686936', 'bacninh157@gmail.com', 'Xóm Ba Quanh, xã Minh Đức, thành phố Phổ Yên, tỉnh Thái Nguyên', 'Xóm Ba Quanh, xã Minh Đức, thành phố Phổ Yên, tỉnh Thái Nguyên', 24, 712, 7197, '2020-03-10 10:30:00+07', '2023-07-28 16:45:36+07', '<p>KN về cấp GCN QSDĐ không đúng đối tượng</p>', 'Ban tiếp công dân', '156', '50', 15049.5, 1, 'Xóm Ba Quanh, xã Minh Đức, thành phố Phổ Yên, tỉnh Thái Nguyên', 24, 712, 7197, '21.417968793490513, 105.86510353345084', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.865001,21.418005],[105.865103,21.417952],[105.865186,21.417982],[105.865189,21.41804],[105.865033,21.418085],[105.865001,21.418005]]]}}', 'Công dân rút nội dung khiếu nại', 1, NULL, '2020-06-09 00:00:00+07', 'Chủ tịch UBND thành phố Phổ Yên', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, true, '{}', '7c613b2d904b4403a3c044c241f57d31', '2023-07-20 16:48:20.385279+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-26 17:13:34.401386+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."Complains" VALUES ('5433aef5-7eda-443e-91bf-695a3b5f7e75', '2020.PYEN.001.', 1, 'Khiếu nại về thu hồi và hủy bỏ GCNQSDĐ', 'Phan Thị Thơm
', '091683062', '1966-10-14 00:00:00+07', '0379087774', 'ptthompy@gmail.com', 'Khu dân cư Ấm, phường Hồng Tiến, TP. Phổ Yên, tỉnh Thái Nguyên
', 'Khu dân cư Ấm, phường Hồng Tiến, TP. Phổ Yên, tỉnh Thái Nguyên
', 24, 712, 4912, '2021-03-04 15:20:16+07', '2021-04-19 15:55:23+07', '<p>Khiếu nại Quyết định số Quyết định số 373/QĐ-UBND ngày 22/01/2020 của UBND thị xã Phổ Yên về việc thu hồi và hủy bỏ giấy chứng nhận QSDĐ ở nông thôn của bà Phan Thị Thơm.</p>', 'Thanh tra thành phố Phổ Yên', '1031
', '91
', 1920, 2, 'Phường Hồng Tiến, thành phố Phổ Yên, tỉnh Thái Nguyên
', 24, 712, 4912, '21.405900, 105.900602', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.90016,21.405854],[105.90016,21.40671],[105.900664,21.40671],[105.900664,21.405854],[105.90016,21.405854]]]}}', 'Đơn đề nghị ngày 22/02/2020 của ông Hồ Minh Linh - người được ủy quyền của bà Phan Thị Thơm về việc sao chụp, sao chép các tài liệu, chứng cứ
', 1, '2020-07-21 00:00:00+07', '2021-02-09 00:00:00+07', 'Chủ tịch UBND huyện Phổ Yên', 'Số 949/QĐ-UBND Thị xã Phổ Yên', 3, NULL, NULL, NULL, NULL, NULL, 0, 0, true, NULL, 'd51343b3ea164814b13309e660ed427d', '2021-12-19 00:00:00+07', NULL, '2023-07-27 01:30:40.119647+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."Complains" VALUES ('3a0c89b4-3b8e-5993-18d7-6dc3cc763fc7', 'KNDD.22.06.2023.3331', 1, 'Khiếu nại cấp GCN', 'Nguyễn Hùng Điềm', '12344449', '1974-06-10 23:00:00+07', '0918173686', 'bacninh157@gmail.com', 'bS', 'bs', 24, 712, 11637, '2023-07-12 15:16:11+07', '2023-07-29 15:16:14+07', '<p>cc</p>', 'Ban tiếp công dân', '123', '91', 333, 1, 'ê', 24, 712, 11637, '21.58774095459925, 105.838833243054', NULL, 'cc', 1, NULL, NULL, 'Chủ tịch UBND huyện Phổ Yên', NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, 0, true, '{}', '5ab6b2e1c0754d7fba12f0a0679cc71e', '2023-07-21 15:18:28.881028+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."Complains" VALUES ('3a0c89b6-ccac-3cbc-6b27-7bf8cbc00d12', 'KNDD.22.6.2023433', 1, 'Khiếu nại cấp GCN', 'Phạm Anh Hải', '12344449', '1967-07-20 23:00:00+07', '0918173686', 'bacninh157@gmail.com', 'tt', 'tt', 24, 712, 11637, '2023-07-13 15:19:11+07', '2023-07-29 15:19:14+07', '<p>tth</p>', 'Ban tiếp công dân', '44', '66', 4566, 1, 'rr', 24, 712, 11637, '21.415179825352197, 105.84892994700893', NULL, 'tt', 1, NULL, NULL, NULL, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, 2, true, '{}', '383f1633ebac437c9f3c5cef544f20c0', '2023-07-21 15:21:17.10111+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."Complains" VALUES ('3a0c851a-4046-c9ed-443a-6fb04fbbcead', 'KNDD.22.06.2023.333', 1, 'Khiếu nại cấp GCN', 'Lê Văn Thiết', '12344449', '2023-07-02 00:00:00+07', '0918173686', 'bacninh157@gmail.com', 'n', 'tn', 24, 712, 7843, '2023-07-11 17:48:45+07', '2023-07-28 17:51:08+07', '<p>tn</p>', 'Ban tiếp công dân', '123', '91', 34, 1, 'tn', 24, 712, 11637, '21.411962608636316, 105.8766656041232', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.866771,21.574517],[105.867179,21.573719],[105.868102,21.574417],[105.867179,21.575116],[105.866771,21.574517]]]}}', 'tn', 1, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, 0, true, '{}', '675e518f1fa04f34946e17789edd1283', '2023-07-20 17:51:48.736346+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-20 17:57:32.177582+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Complains" VALUES ('3a0c84e3-3286-d91d-a62a-da3a5a57fb7c', 'KNDD.22.06.2023.45', 1, 'Khiếu nại cấp GCN', 'Hoàng Văn Tân', '12344449', '2023-07-04 00:00:00+07', '0918173686', 'bacninh157@gmail.com', 'tn', 'tn', 24, 712, 7843, '2023-07-11 16:49:32+07', '2023-07-29 16:49:34+07', '<p>tn</p>', 'Ban tiếp công dân', '123', '91', 455, 1, 'tn', 24, 706, 8836, '21.415179825352197, 105.84892994700893', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.848297,21.415454],[105.849392,21.414735],[105.849692,21.415534],[105.849306,21.416353],[105.848297,21.415454]]]}}', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, true, '{}', 'ac346ede1be44aceb6eb83ff6c46faa1', '2023-07-20 16:51:40.678703+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-20 17:57:41.698117+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Complains" VALUES ('3a0c84b4-8f63-243c-9ed2-92b74f4c6f53', 'KNDD.22.06.2023.', 1, 'Khiếu nại cấp GCN', 'Đàm Thị Nga', '12344449', '2000-07-20 00:00:00+07', '0918173686', NULL, 'thái ', 'thái', 24, 712, 11637, '2023-07-04 15:59:53+07', '2023-07-29 15:59:57+07', '<p>kn</p>', 'Ban tiếp công dân', '123', '455', 445, 1, 'thái', 24, 712, 7843, '21.417968793490513, 105.86510353345084', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.864913,21.418075],[105.865009,21.417892],[105.865219,21.417915],[105.86535,21.418025],[105.864956,21.418152],[105.864913,21.418075]]]}}', 'kn', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, true, '{}', 'f8908474a60e4cb3811f8d6fd3d45ce5', '2023-07-20 16:00:44.271604+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-20 17:57:49.947422+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');




INSERT INTO "KNTC"."Denounces" VALUES ('9a84e653-f3f0-47d2-bc25-a8ebfba52801', '05878.15.648a
', 1, 'Tố cáo Chủ tịch phường Đồng Tiến cố ý làm sai, bao che cấp dưới
', 'Phạm Văn Hải
', '12345678901
', '1980-04-13 00:00:00+07', '0918134567', 'hainf@gmail.com', 'X. Đông Tiến
', 'X. Đông Tiến
', 24, 712, 3363, '2020-07-18 09:05:00+07', '2020-08-10 19:13:00+07', 'Tố cáo Chủ tịch phường Đồng Tiến cố ý làm sai, bao che cấp dưới
', 'Nguyễn Đình Trạc', 'Huyện ủy xã', '648a
', '19
', 100, 5, 'Xã Đồng Tiến', 24, 712, 3363, '21.46083315890784, 105.96296127952964', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.962147,21.459967],[105.962147,21.461475],[105.967917,21.461475],[105.967917,21.459967],[105.962147,21.459967]]]}}', 'Ban tiếp công dân gửi TTTP xem xét, giải quyết
', '2022-01-01 00:00:00+07', '', '', '2022-01-01 00:00:00+07', '', '2022-01-01 00:00:00+07', '2022-01-01 00:00:00+07', '', '2022-01-01 00:00:00+07', 2, false, NULL, 'e1935a615aab4931b9c053f2010be3af', '2021-07-19 00:00:00+07', NULL, '2023-05-14 22:56:26.120778+07', '3a0acb59-5a9a-3705-038e-5c72984d35f7');
INSERT INTO "KNTC"."Denounces" VALUES ('3a0acb82-6650-5d89-fd2c-f2ee6b56f5b8', '213/HSe1234', 2, 'Tố cáo công ty Đức Minh xả thải ra hồ', 'Khúc Thị', '21321321312', '2023-04-24 00:00:00+07', '0913123153', NULL, 'edsdsa dsad', 'dsad dsad', 24, 706, 1775, '2023-04-05 23:52:15+07', '2023-04-17 23:52:19+07', '<p>saSs</p>', '<p>saSs</p>', 'Công An phường', '2321', '12213', 1000, 4, 'dsadas dsada', 24, 706, 1775, '21.609052754395176, 105.59049946191041', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.590306,21.608793],[105.590306,21.609372],[105.591014,21.609372],[105.591014,21.608793],[105.590306,21.608793]]]}}', 'saSa', '-infinity', NULL, NULL, '-infinity', NULL, NULL, NULL, NULL, '-infinity', 0, true, '{}', 'dfef1c41e14f4304b333d35c84369d00', '2023-04-25 23:53:24.752194+07', '3a0acb59-5a9a-3705-038e-5c72984d35f7', '2023-05-14 23:38:09.293036+07', '3a0acb59-5a9a-3705-038e-5c72984d35f7');
INSERT INTO "KNTC"."Denounces" VALUES ('3a0c89ba-6e92-1e9d-b3fe-6cbfd95fb074', 'tc23', 1, 'tố cáo về đất đai', 'Ngô Văn Tách', '90687759', '1970-10-23 23:00:00+07', '0918173686', NULL, 'tn', 'tn', 24, 712, 11637, '2023-07-18 15:23:01+07', '2023-07-27 15:23:04+07', '<p>ttt</p>', '<p>ttt</p>', 'Ban tiếp công dân', '123', '455', 4455, 1, '555', 24, 712, 11637, '21.415179825352197, 105.84892994700893', NULL, 'ttt', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, true, '{}', '7da79ab99ea14db88af045a80c2c0613', '2023-07-21 15:25:15.15989+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."Denounces" VALUES ('27616bea-f65b-a96c-5198-3a0ab54090fe', '21321/CDHA', 3, 'Tố cáo công ty Đỗ Bãn xả thải ra hồ Núi Cốc', 'Nguyễn Hiệu', '23123213', '2000-04-18 00:00:00+07', '0902392031', 'thantien78@gmail.com', 'dsa dá', 'dsa dsa', 24, 433, 9562, '2023-04-11 16:08:05+07', '2023-04-12 16:08:03+07', '<p>Tôi thấy tận mắt (có quay phim làm chứng) hành động xả thải ra môi trường của công ty</p>', '<p>Tôi thấy tận mắt (có quay phim làm chứng) hành động xả thải ra môi trường của công ty</p>', 'Ủy Ban Huyện', '121', '121', 3213, 4, '2edsa dsadas', 24, 433, 9562, '21.58399369677468, 105.70264987286505', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.700505,21.583515],[105.700505,21.586627],[105.705224,21.586627],[105.705224,21.583515],[105.700505,21.583515]]]}}', 'dsadas', '2001-01-02 00:00:00+07', NULL, NULL, '2002-01-22 00:00:00+07', NULL, NULL, NULL, NULL, '2014-07-18 00:00:00+07', 3, true, '{}', 'f400822fe3a040f78765b5ae9edb1763', '2023-04-21 16:09:51+07', '6995b140-27c7-e4a4-c857-3a0a5a041363', '2023-07-19 10:18:09.786802+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Denounces" VALUES ('37c703dd-ca0f-4c7e-bc77-04ceebeda07d', '9090.9090.87', 4, 'Tố cáo hàng xóm đổ bậy vào đầu nguồn nước', 'Lê Văn Test', '01232132113', '1935-01-11 00:00:00+07', '0121921021', 'aicha@gmail.com', 'X. Đồng Tiến', 'Đông Khối', 24, 712, 3363, '2001-12-12 00:00:00+07', '2021-12-12 00:00:00+07', 'Trẻ con mất lòng người lớn', 'Nguyễn Văn Thể', 'Bí thư Tỉnh Ủy', '423b', '15', 12, 5, 'Đồng Lòng', 24, 712, 3363, '21.429444, 105.894392', '{"type":"Feature","properties":{},"geometry":{"type":"Polygon","coordinates":[[[105.894074,21.42926],[105.894074,21.430608],[105.895323,21.430608],[105.895323,21.42926],[105.894074,21.42926]]]}}', 'Công an Xã', '2022-01-01 00:00:00+07', '', '', '2022-01-01 00:00:00+07', NULL, NULL, NULL, '', '2022-01-01 00:00:00+07', 1, true, NULL, 'c323c1a6d5e44926ba9dcb513f9e4a01', '2011-05-12 00:00:00+07', NULL, '2023-07-19 16:15:21.75129+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."Denounces" VALUES ('3a0c89c1-e2d1-8023-5f37-dc7a7e80b9d7', 'tc45', 1, 'tố cáo về đất đai', 'Dương Văn Chúc', '12344449', '2023-07-02 00:00:00+07', '0918173686', NULL, 'th', 'tc', 24, 712, 11356, '2023-07-24 15:30:35+07', '2023-07-29 15:30:37+07', '<p>tt</p>', '<p>tt</p>', 'Ban tiếp công dân', '123', '455', 444, 1, '55', 24, 712, 11356, '21.415179825352197, 105.84892994700893', NULL, 'tt', '2023-07-19 00:00:00+07', 'Dương Văn Chúc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, true, '{}', '32b075b15d304eae972d7482d6196ef8', '2023-07-21 15:33:23.666358+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."Denounces" VALUES ('3a0c89bc-ee7d-3768-2743-2cc14d25e150', 'tc34', 1, 'tố cáo về đất đai', 'Lê Quang Hải', '22', '1958-10-01 00:00:00+07', '0979938268', NULL, 'Xóm Na Lang 2, xã Thành Công, thị xã Phổ Yên, tỉnh Thái Nguyên
', 'Xóm Na Lang 2, xã Thành Công, thị xã Phổ Yên, tỉnh Thái Nguyên
', 24, 712, 11356, '2020-08-17 16:10:00+07', '2020-09-17 15:26:00+07', '<p> Tố cáo ông Nguyễn Văn Thiện, Phó bí thư Đảng ủy - PCT UBND xã Thành Công có những vi phạm: Vi phạm trong quản lý chợ Long Thành; lấn chiếm đất công thuộc hồ Xuân Hà; nhận đấu thầu đất trái thẩm quyền thuộc đất Hồ Xuân Hà; tự ý đổ đất xuống ruộng trồng lúa để trồng cây ăn quả&nbsp;</p>', 'Nguyễn Văn Thiện', 'Ban tiếp công dân', '236', '50', 363, 1, 'Xã Thành Công', 24, 712, 11356, '21.58774095459925, 105.838833243054', NULL, 'Quyết định thụ lý TC số 5197/QĐ-UBND ngày 17.8.2020

Gia hạn theo Quyết định số 5974/QĐ-UBND ngày 28.9.2020 về gia hạn giải quyết tố cáo
Số VB KLNDTC Số 10/KL-UBND ngày 26/11/2020

', '-infinity', 'Chủ tịch UBND thị xã Phổ Yên', NULL, '-infinity', NULL, NULL, NULL, NULL, '-infinity', 2, true, '{}', 'b7b4b672812b411a90218047f169fe78', '2023-07-21 15:27:58.97389+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-21 16:29:24.949193+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');


INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca3c5-9924-f091-4957-1ffc11af7902', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Biên bản làm việc', 1, '2020-05-25 15:30:00+07', '2020-05-25 15:30:00+07', '6', '<p>Biên bản làm việc, giải quyết nội dung khiếu nại của công dân Đoàn Văn Hùng, xóm Ba Quanh, xã Minh Đức</p>', 'Bien ban lam viec.pdf', 'application/pdf', 360959, 1, false, '{}', 'fc8ed86705a14ec3bb8a9266a219b186', '2023-07-26 16:47:34.774728+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 16:53:55.124918+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0c851c-1c60-6aac-db80-c19341f07fd1', '3a0c851a-4046-c9ed-443a-6fb04fbbcead', NULL, 1, 'Giấy chứng nhận QSDĐ', 3, '2023-07-25 17:52:14+07', '2023-07-04 17:52:05+07', '1', '<p>Ho so</p>', 'FILE_20230213_110422_HS KN ông Lê Văn Thiết_02_13_2023.pdf', 'application/pdf', 98874439, 1, true, '{}', 'a78dcd93c15a4046afa5c02c09b174a4', '2023-07-20 17:53:50.639238+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', '2023-07-20 18:00:56.114016+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0c89c3-89b7-d151-4cf7-e0a0a6d34fef', NULL, '3a0c89c1-e2d1-8023-5f37-dc7a7e80b9d7', 0, 'Giấy chứng nhận QSDĐ', 3, '2023-07-06 15:33:47+07', '2023-07-04 15:33:42+07', '23', '<p>tt</p>', 'IMG_0003.pdf', 'application/pdf', 215096, 2, true, '{}', 'ee80155176164f448bc82609c534c3d9', '2023-07-21 15:35:11.927634+07', '3a0bbd8b-ec41-c534-4eb9-4c02f7613246', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca38b-2ad1-608f-223d-adaa80b88a6e', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Hợp đồng ủy quyền', 1, '2020-03-05 15:00:00+07', '2020-03-23 15:15:00+07', '2', '<p>Hợp đồng ủy quyền</p>', 'Hop dong uy quyen.pdf', 'application/pdf', 851396, 1, false, '{}', '825bbff7e2fb4c3ab25a2f05fdcba4e3', '2023-07-26 15:43:45.233757+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 16:54:33.370665+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca385-efef-cd2e-ed77-a0caa640b4be', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Quyết định xác minh', 1, '2020-03-24 09:00:00+07', '2020-03-24 09:00:00+07', '3', '<p>Quyết định số 1374/QĐ-UBND ngày 24/3/2020 về việc xác minh nội dung khiếu nại của ông Đoàn Thế Hùng, thường trú xóm Ba Quanh, xã Minh Đức, thị xã Phổ Yên</p>', 'Quyet dinh xac minh vu viec.pdf', 'application/pdf', 459594, 1, false, '{}', '7156ae2aec654d58b0c600389158b1ec', '2023-07-26 15:38:02.480449+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 16:54:40.534491+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca3ca-6aec-43d2-609f-b32089f47559', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Quyết định thụ lý', 1, '2020-03-24 09:00:00+07', '2020-03-24 09:00:00+07', '4', '<p>Thông báo về việc thụ lý giải quyết khiếu nại lần 1</p>', 'Quyet dinh thu ly vu viec.pdf', 'application/pdf', 310982, 1, false, '{}', '3d1bf9c34f004fabb159a67b530fa52a', '2023-07-26 16:52:50.413376+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 16:54:49.002649+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca3cf-1bfe-18ca-7667-4e63817eb05b', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Quyết định đình chỉ', 1, '2020-06-09 09:00:00+07', '2020-06-09 09:00:00+07', '7', '<p>Quyết định về việc đình chỉ giải quyết khiếu nại</p>', 'Quyet dinh dinh chi.pdf', 'application/pdf', 238763, 1, false, '{}', '7a19481504574f6e864fd25c5fb617ae', '2023-07-26 16:57:57.887594+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca384-0d3f-d7bb-52c3-540d50ec595b', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Đơn khiếu nại', 1, '2020-03-10 09:00:00+07', '2020-03-10 09:00:00+07', '1', '<p>Đơn khiếu nại của ông Đoàn Văn Hùng xóm Ba Quanh, xã Minh Đức, thị xã Phổ Yên</p>', 'Don khieu nai.pdf', 'application/pdf', 448789, 1, false, '{}', '7317d8860ab14f17a4353dab43b58c36', '2023-07-26 15:35:58.918041+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca49a-1af6-d99e-97be-c444ab875d47', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Đơn đề nghị', 3, '2020-04-08 09:00:00+07', '2020-04-10 09:00:00+07', '3', '<p>Đơn đề nghị làm rõ việc thu hồi và hủy bỏ Giấy chứng nhận quyền sử dụng đất của bà Phan Thị Thơm theo Quyết định số 373/QĐ-UBND ngày 22/01/2020 của UBND thị xã Phổ Yên, tỉnh Thái Nguyên</p>', 'don de nghi.pdf', 'application/pdf', 1075289, 1, true, '{}', 'cc72a27a078142ff87f716a2a99e9721', '2023-07-26 20:39:41.430922+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 20:56:19.92879+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca39c-7fb4-22da-7b4c-b82af995dd48', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Báo cáo giải quyết đơn khiếu nại', 1, '2020-05-20 09:00:00+07', '2020-05-20 09:00:00+07', '5', '<p>Báo cáo về việc giải quyết đơn của ông Đoàn Thế Hùng, xóm Ba Quanh, xã Minh Đức</p>', 'Bao cao giai quyet.pdf', 'application/pdf', 259092, 1, false, '{}', '1e18690a45cd4f98a23b45c0bbf735be', '2023-07-26 16:02:41.076568+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 16:53:47.870763+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca3d0-dd0d-3f4c-5086-4aa305d0acae', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Đơn xin rút khiếu nại', 1, '2020-06-09 09:00:00+07', '2020-06-09 09:00:00+07', '8', '<p>Đơn xin rút khiếu nại</p>', 'Don rut khieu nai.pdf', 'application/pdf', 397744, 1, false, '{}', 'f33c178d238a4cdc96b59ade22bf1224', '2023-07-26 16:59:52.845707+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca3dd-4f92-b167-c1e0-10dd77e7a769', '3a0c84e0-2420-55cb-f730-26afe1cb2d37', NULL, 1, 'Báo cáo giải quyết ', 1, '2020-06-09 15:00:00+07', '2020-06-09 15:00:00+07', '9', '<p>Báo cáo giải quyết đơn của UBND</p>', 'Bao cao giai quyet cua UBND.jpg', 'image/jpeg', 261064, 1, false, '{}', '0c5f7197cf9d4787b6e746622ce02aa3', '2023-07-26 17:13:28.594798+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca3fa-ec31-1036-093e-a1a32550f9dc', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Don de nghi', 1, '2020-02-22 09:00:00+07', '2021-03-04 09:00:00+07', '2', '<p>Đơn đề nghị ngày 22/02/2020 của ông Hồ Minh Linh - người được ủy quyền của bà Phan Thị Thơm về việc sao chụp, sao chép các tài liệu, chứng cứ</p>', 'don de nghi sao chep, chup.pdf', 'application/pdf', 226311, 1, true, '{}', '8ce0bf698aa842109801fe299f814325', '2023-07-26 17:45:49.234385+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 20:56:31.730594+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca4a9-0001-2f1d-5bc1-233a41a0ec6e', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Quyết định UBND thị xã', 3, '2020-01-22 09:00:00+07', '2020-01-22 09:00:00+07', '1', '<p>Quyết định số 373/QĐ-UBND ngày 22/01/2020 của UBND thị xã Phổ Yên về việc thu hồi và hủy bỏ Giấy chứng nhận QSDĐ của bà Phan Thị Thơm, ở xóm Ấm, xã Hồng Tiến, thị xã Phổ Yên</p>', 'quyet dinh thu hoi va huy bo GCN.pdf', 'application/pdf', 599144, 1, true, '{}', '577283a85ba5402ca49224e8f9585d2d', '2023-07-26 20:55:57.56978+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca491-04ec-9b9e-2a81-4417862d8a51', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Đơn khiếu nại', 3, '2020-05-25 09:00:00+07', '2020-06-03 09:00:00+07', '5', '<p>Đơn khiếu nại về việc UBND thị xã Phổ Yên thu hồi và hủy bỏ giấy chứng nhận QSDĐ của bà Phan Thị Thơm, ở xóm Ấm, xã Hồng Tiến, thị xã Phổ Yên</p>', 'don khieu nai.pdf', 'application/pdf', 2179815, 1, true, '{}', 'd2c805de3ada4efcbffd9368059a8604', '2023-07-26 20:29:46.021374+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 20:56:08.44707+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca4a1-d630-e510-0535-f1cfa17acaab', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Văn bản trả lời đơn đề nghị', 3, '2020-05-07 09:00:00+07', '2020-05-07 09:00:00+07', '4', '<p>Văn bản số 650/UBND-TNMT ngày 07/5/2020 trả lời đơn đề nghị của bà Phan Thị Thơm ở xóm Ấm, xã Hồng Tiến</p>', 'traloidon de nghi.pdf', 'application/pdf', 2327016, 1, true, '{}', '1af83a470272427a9eb16db9db0cc50e', '2023-07-26 20:48:08.113475+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-26 20:56:14.205831+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca577-efae-e535-d296-d035ffe27414', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Quyết định xác minh ', 2, '2020-09-22 09:00:00+07', '2020-09-22 09:00:00+07', '6', '<p>Quyết định số 5840/QĐ-UBND ngày 22/9/2020 về việc xác minh nội dung khiếu nại của bà Phan Thị Thơm xóm Ấm, xã Hồng Tiến, thị xã Phổ Yên, tỉnh Thái Nguyên</p>', 'quyet dinh xac minh khieu nai.pdf', 'application/pdf', 519789, 1, true, '{}', 'f200f96568524936aca5ad5c2501e475', '2023-07-27 00:41:59.393386+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca57e-881d-fb4f-fbe6-8ba9c85245b4', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Đơn yêu cầu', 1, '2020-11-26 09:00:00+07', '2020-12-15 15:00:00+07', '7', '<p>Đơn yêu cầu ngày 26/11/2020 của bà Phan Thị Thơm</p>', 'don yeu cau.pdf', 'application/pdf', 229654, 1, true, '{}', 'd1e4f3b49b844a2e96564a98746e0f69', '2023-07-27 00:49:11.582691+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca582-12bf-c490-0534-2cfe6454329a', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Báo cáo ', 1, '2020-11-05 09:00:00+07', '2020-11-05 09:00:00+07', '8', '<p>Báo cáo số 01/BC-ĐXM ngày 05/11/2020 của Đoàn xác minh về việc xác minh nội dung khiếu nại của bà Phan Thị Thơm xóm Ấm, xã Hồng Tiến, thị xã Phổ Yên, tỉnh Thái Nguyên</p>', 'bao cao ĐXM.pdf', 'application/pdf', 518047, 1, true, '{}', '1c96041d8a1441eb931d19a71d3af53b', '2023-07-27 00:53:03.680257+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca585-3c0e-92e6-a915-f72a9dbebe93', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Biên bản đối thoại', 1, '2021-01-27 09:00:00+07', '2021-01-27 09:00:00+07', '9', '<p>Biên bản đối thoại giải quyết khiếu nại ngày 27/01/2021</p>', 'bien ban doi thoai.pdf', 'application/pdf', 1059418, 1, true, '{}', 'a2c14eadddac4ae8b00041d20ee42ab1', '2023-07-27 00:56:30.863556+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca58b-0756-3dbd-f05a-960717d3ffb6', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Báo cáo xác minh', 1, '2021-01-28 09:00:00+07', '2021-01-28 09:00:00+07', '10 ', '<p>Báo cáo số 16/BC-TNMT ngày 28/01/2021 của Phòng TNMT báo cáo kết quả xác minh vụ việc tranh chấp đất đai giữa bà Dương Thị Tâm và bà Phan Thị Thơm ở xóm Ấm, xã Hồng Tiến, thị xã Phổ Yên</p>', 'bao cao TNMT.pdf', 'application/pdf', 1694661, 1, true, '{}', '9233bf35c97d44578c0719e9a41c5d4a', '2023-07-27 01:02:50.58321+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', '2023-07-27 01:03:21.477526+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b');
INSERT INTO "KNTC"."FileAttachments" VALUES ('3a0ca58e-8258-e0ee-69b0-d0ef9cdee06b', '5433aef5-7eda-443e-91bf-695a3b5f7e75', NULL, 1, 'Quyết định giải quyết khiếu nại', 2, '2021-02-09 09:00:00+07', '2021-02-09 09:00:00+07', '11', '<p>Quyết định số 949/QĐ-UBND ngày 09/02/2021 của UBND thị xã Phổ Yên về việc giải quyết nội dung khiếu nại của bà Phan Thị Thơm thường trú xóm Ấm, xã Hồng Tiến, thị xã Phổ Yên (lần đầu)</p>', 'quyet dinh giai quyet KN.pdf', 'application/pdf', 3802038, 1, true, '{}', '756184d13c4c4a00b2dc6e646b9b0811', '2023-07-27 01:06:38.680663+07', '3a0ca282-f6d3-5af7-de36-8d7b987f275b', NULL, NULL);






