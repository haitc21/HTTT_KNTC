SELECT "Id", "ClientId", "ClientSecret", "ConsentType", "DisplayName", "DisplayNames", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Type", "ClientUri", "LogoUri", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime"
-- 	FROM public."OpenIddictApplications";
-- SELECT "Id", "ApplicationId", "CreationDate", "Properties", "Scopes", "Status", "Subject", "Type", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime"
-- 	FROM public."OpenIddictAuthorizations";
-- SELECT "Id", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime"
-- 	FROM public."OpenIddictScopes";SELECT "Id", "Description", "Descriptions", "DisplayName", "DisplayNames", "Name", "Properties", "Resources", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime"
-- 	FROM public."OpenIddictScopes";
-- SELECT "Id", "ApplicationId", "AuthorizationId", "CreationDate", "ExpirationDate", "Payload", "Properties", "RedemptionDate", "ReferenceId", "Status", "Subject", "Type", "ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "IsDeleted", "DeleterId", "DeletionTime"
-- 	FROM public."OpenIddictTokens";
	
	TRUNCATE TABLE public."OpenIddictApplications";
	TRUNCATE TABLE public."OpenIddictAuthorizations";
	TRUNCATE TABLE public."OpenIddictScopes";
	TRUNCATE TABLE public."OpenIddictTokens";