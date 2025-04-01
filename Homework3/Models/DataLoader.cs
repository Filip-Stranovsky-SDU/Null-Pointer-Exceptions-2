using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

public static class DataLoader 
{
    public static List<T> LoadData<T>(string path)
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var data = csv.GetRecords<T>().ToList();
            return data;
        }
    }
}