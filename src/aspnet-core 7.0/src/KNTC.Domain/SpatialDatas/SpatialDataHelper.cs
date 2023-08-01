using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Volo.Abp;

namespace KNTC.SpatialDatas;

public static class SpatialDataHelper
{
    public static Geometry? ConvertJsonToGeometry(string? json)
    {
        if (json.IsNullOrEmpty()) return null;
        var serializer = GeoJsonSerializer.Create();
        using (var stringReader = new StringReader(json))
        using (var jsonReader = new JsonTextReader(stringReader))
        {
            Geometry result = serializer.Deserialize<Geometry>(jsonReader);
            return result;
        }
    }

    public static string ConvertGeometryToJson(Geometry? geometry)
    {
        if (geometry == null) return string.Empty;
        var serializer = GeoJsonSerializer.Create();
        using (var stringWriter = new StringWriter())
        using (var jsonWriter = new JsonTextWriter(stringWriter))
        {
            serializer.Serialize(jsonWriter, geometry);
            return stringWriter.ToString();
        }
    }

    public static string ConvertGeoDataToJson(GeoJsonData geoJsonData)
    {
        if (geoJsonData == null) return string.Empty;
        return JsonConvert.SerializeObject(geoJsonData, Formatting.None);
    }

    public static GeoJsonData? ConvertJsonToGeoData(string? json)
    {
        if (json.IsNullOrEmpty()) return null;
        var reader = new NetTopologySuite.IO.GeoJsonReader();
        var result = reader.Read<GeoJsonData>(json);
        return result;
    }

    public static Point? ConvertStringToPoint(string? latLng)
    {
        if (latLng.IsNullOrEmpty()) return null;
        // Step 1: Split the input string
        string[] parts = latLng.Split(',');

        // Step 2: Parse latitude and longitude as decimal
        if (parts.Length != 2 || !decimal.TryParse(parts[0], NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out decimal latitude) ||
            !decimal.TryParse(parts[1], NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out decimal longitude))
        {
            throw new UserFriendlyException("Dữ liệu tọa độ sai định dạng 'Vĩ độ (lat), Kinh độ (lng)'");
        }
        // Step 3: Create Coordinate using latitude and longitude
        Coordinate coordinate = new Coordinate((double)longitude, (double)latitude);

        // Step 4: Create Point using GeometryFactory and the coordinate
        GeometryFactory geometryFactory = new GeometryFactory();
        Point point = geometryFactory.CreatePoint(coordinate);

        return point;
    }

    public static string ConvertPointToString(Point point)
    {
        if (point == null) return string.Empty;
        // Step 1: Extract latitude and longitude from the Point object
        double latitude = point.Coordinate.Y;
        double longitude = point.Coordinate.X;

        // Step 2: Format latitude and longitude as strings
        string latString = latitude.ToString("G", CultureInfo.GetCultureInfo("en-US"));
        string lngString = longitude.ToString("G", CultureInfo.GetCultureInfo("en-US"));

        // Step 3: Concatenate latitude and longitude with a comma separator
        string latLngString = $"{latString}, {lngString}";

        return latLngString;
    }
}