-- DocumentTypes
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "KNTC"."DocumentTypes";
  EXECUTE 'ALTER SEQUENCE "Sequence-DocumentType" RESTART WITH ' || max_id + 1;
END $$;

-- BaseMaps
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "KNTC"."BaseMaps";
  EXECUTE 'ALTER SEQUENCE "Sequence-BaseMap" RESTART WITH ' || max_id + 1;
END $$;

-- LandTypes
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "KNTC"."LandTypes";
  EXECUTE 'ALTER SEQUENCE "Sequence-LandType" RESTART WITH ' || max_id + 1;
END $$;

-- SysConfigs
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "AppSysConfigs";
  EXECUTE 'ALTER SEQUENCE "Sequence-SysConfig" RESTART WITH ' || max_id + 1;
END $$;

-- Units
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "KNTC"."Units";
  EXECUTE 'ALTER SEQUENCE "Sequence-Unit" RESTART WITH ' || max_id + 1;
END $$;

-- UnitTypes
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "KNTC"."UnitTypes";
  EXECUTE 'ALTER SEQUENCE "Sequence-UnitType" RESTART WITH ' || max_id + 1;
END $$;

-- SpatialDatas
DO $$
DECLARE
  max_id integer;
BEGIN
  SELECT MAX("Id") INTO max_id FROM "SPATIAL_DATA"."SpatialDatas";
  EXECUTE 'ALTER SEQUENCE "Sequence-SpatialData" RESTART WITH ' || max_id + 1;
END $$;