using PharmacyDB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DbInitialize
    {
        public static async Task Initialize(ApiContext context)
        {
            if (!await context.Database.CanConnectAsync() || context.Pharms.Any())
            {
                return;
            }

            await context.Pharms.AddRangeAsync(
                Enumerable.Range(0, 4).Select(_ => new Pharmaceutical
                {
                    Name = Faker.Company.Name(),
                })
            );

            await context.SaveChangesAsync();
        }
    }
}
