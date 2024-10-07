using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
	public class SearchOnGithubContext: DbContext
	{
        public SearchOnGithubContext(DbContextOptions<SearchOnGithubContext> options)
            : base(options)
        {
        }

        public DbSet<SearchQuery> SearchQuery { get; set; }
	}
}
