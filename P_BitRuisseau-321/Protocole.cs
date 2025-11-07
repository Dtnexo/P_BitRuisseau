using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace P_BitRuisseau_321
{
    internal class Protocole
    {
        string sourcePath = "Résaux";
        string pathToSourceFile = Path.Combine(sourcePath, "ex.json");
        var list = JsonConvert.DeserializeObject<List<Gesamtplan>>(File.ReadAllText(pathToSourceFile));
    }
}
