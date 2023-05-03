DELETE FROM "KNTC"."Units"  where "Units"."Id" not in (24,189,294,351,368,433,465,517,706,712) 
and "Units"."ParentId" not in (24,189,294,351,368,433,465,517,706,712)

DELETE FROM "KNTC"."Units"
WHERE "Units"."ParentId" is null and "Units"."Id"!=24
