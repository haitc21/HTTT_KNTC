-- SELECT * FROM phoyen_polygons;

UPDATE "phoyen_polylines" 
SET geom = ST_Transform(ST_SetSRID(geom, 3857), 4326);

ALTER TABLE phoyen_polylines 
ALTER COLUMN geom TYPE geometry(MULTILINESTRING, 4326);