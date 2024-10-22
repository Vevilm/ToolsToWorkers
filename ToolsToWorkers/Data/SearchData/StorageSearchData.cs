﻿using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data.SearchData
{
    public class StorageSearchData : SearchData
    {
        public IEnumerable<Storage> Storages { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "Все";
        public string Status { get; set; } = "Все";


        public string ToString(int i)
        {
            return Name + "/{}/" + Type + "/{}/" + Status + "/{}/" + i + "/{}/" + PageSize + "/{}/" + elementsCount;
        }
        public static StorageSearchData GetSearchData(string sypher)
        {
            var data = sypher.Split("/{}/");
            StorageSearchData result = new StorageSearchData();
            if (data.Length > 1)
                result = new StorageSearchData()
                {
                    Name = data[0],
                    Type = data[1],
                    Status = data[2],
                    PageNumber = Convert.ToInt32(data[3]),
                    PageSize = Convert.ToInt32(data[4]),
                    elementsCount = Convert.ToInt32(data[5])
                };
            return result;
        }
    }
}
