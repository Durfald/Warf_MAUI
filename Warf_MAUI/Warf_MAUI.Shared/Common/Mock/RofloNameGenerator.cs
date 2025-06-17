namespace Warf_MAUI.Shared.Common.Mock
{
    public static class RofloNameGenerator
    {
        private static readonly List<string> Prefix = [
            "The",
            "Creamest",
            "Sexiest",
            "Hottest",
            "Super",
            "Leather man's",
            "Roflo",
            "The greatest",
            "Shitty",
            "Useless",
            "A",
            "Terrorristic",
            "Jewish",
            "Atomic"
        ];

        private static readonly List<string> Name = [
            "Shiish",
            "Kebab (landmine)",
            "Lether glove",
            "Hot load",
            "Braga",
            "Heart",
            "Dukat",
            "Dickat",
            "Muscilar Item",
            "Item",
            "Stuff",
            "Garbage",
            "Shit",
            "1,2,3-trinitroxypropane"
        ];

        private static readonly List<string> Suffix = [
            "of Nice time",
            "of Poority",
            "of Horrible parenthood",
            "of two",
            "of Old ones",
            "of Arsen's legacy",
            "from Markaronya",
            "from Your mom",
            "from dog of wisdom",
            "used by VAGA"
        ];
        
        public static string GenerateRofloName()
        {
            var name = string.Empty;
            if (Random.Shared.Next(10) >= 5)
            {
                name += Prefix[Random.Shared.Next(Prefix.Count)];
                name += ' ';
            }

            name += Name[Random.Shared.Next(Name.Count)];
            
            if (Random.Shared.Next(10) >= 7)
            {
                name += ' ';
                name += Suffix[Random.Shared.Next(Suffix.Count)];
            }

            return name;
        }
    }
}
