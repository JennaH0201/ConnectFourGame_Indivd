using System;
using System.IO;
using System.Text.Json;

namespace ConnectFour
{
    public class SerializableDataField
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public string[] Grid { get; set; } = Array.Empty<string>();
        public int Turn { get; set; }

        public string Player1DiscType { get; set; } = "";
        public string Player2DiscType { get; set; } = "";
        public string Player1Symbol { get; set; } = "";
        public string Player2Symbol { get; set; } = "";
    }

    public class SaveData
    {
        private string saveFilePath = "save_game.json";

        public void SaveGame(SerializableDataField data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(saveFilePath, json);
            Console.WriteLine("Your game has been saved!");
        }


    }
}