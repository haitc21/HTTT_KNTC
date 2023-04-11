delete FROM [KNTC].[KNTC].[Units]  where id not in (24,189,294,351,368,433,465,517,706,712) and ParentId not in (24,189,294,351,368,433,465,517,706,712)
delete FROM [KNTC].[KNTC].[Units]
where ParentId is null and id!=24