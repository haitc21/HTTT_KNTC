using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public static Point? ConvertStringToPoint(string? input)
    {
        if (input.IsNullOrEmpty()) return null;
        var arrCoor = input.Split(", ");
        double lat = double.Parse(arrCoor[0]);
        double lng = double.Parse(arrCoor[1]);
        return new Point(lat, lng);
    }
    public static string ConvertPointToString(Point point)
    {
        if (point == null) return string.Empty;
        var lat = point.CoordinateSequence.First;
        var lng = point.CoordinateSequence.Last;
        return $"{lat}, {lng}";
    }
}
