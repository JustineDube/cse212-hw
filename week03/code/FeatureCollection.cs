// TODO Problem 5 - ADD YOUR CODE HERE
// The USGS GeoJSON feed has the following structure:
// {
//   "type": "FeatureCollection",
//   "features": [
//     {
//       "type": "Feature",
//       "properties": {
//         "mag": 2.36,
//         "place": "1km NE of Pahala, Hawaii",
//         ...
//       },
//       ...
//     },
//     ...
//   ]
// }

public class FeatureCollection
{
    public List<Feature> Features { get; set; } = [];
}

public class Feature
{
    public FeatureProperties Properties { get; set; } = new();
}

public class FeatureProperties
{
    public string Place { get; set; } = "";
    public double? Mag { get; set; }
}