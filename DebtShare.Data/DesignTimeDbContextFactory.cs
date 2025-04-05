using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DebtShare.Data
{
    //This exists because of the Repositories.
    public class DesignTimeDbContextFactory
            : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // 1) Зареждаме appsettings.json от текущата директория.
            //    При design-time (когато правим миграции), EF ще извика този метод.
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // 2) Извличаме connection string
            //    Уверете се, че имате "ConnectionStrings:DefaultConnection" в appsettings.json
            var connectionString = config.GetConnectionString("DefaultConnection");

            // 3) Създаваме options builder
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);

            // 4) Връщаме новия контекст
            return new ApplicationDbContext(builder.Options);
        }
    }
}
