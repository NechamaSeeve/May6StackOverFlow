using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace May6StackOverFlow.Data
{
    public class QuestionAnswerContextFactory : IDesignTimeDbContextFactory<QuestionAnswerContext>
    {
        public QuestionAnswerContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
              $"..{Path.DirectorySeparatorChar}May6StackOverFlow.Web"))
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new QuestionAnswerContext(config.GetConnectionString("ConStr"));
        }

    }
    
}
