using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        public void Intialize()
        {
            throw new NotImplementedException();
        }

        private void SeedDataFromJson <T, TKey> (string fileName , DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\ECommerce.Presistance\Data\DataSeed\JSON Files\" + fileName;

            if (!File.Exists(FilePath)) return;

            try
            {
                using var dataStream = File.OpenRead(FilePath);

                var data = JsonSerializer.Deserialize<List<T>>(dataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (data is not null) 
                {
                    dbset.AddRange(data);
                }


            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error While Reading JSON File {ex}");

                return;
            }
        }
    }
}
