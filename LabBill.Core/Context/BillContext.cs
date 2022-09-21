using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabBill.Core.Domains;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LabBill.Core.Context;
public class BillContext : DbContext
{
    public DbSet<Bill> Bills
    {
        get; set;
    }

    public DbSet<Person> People
    {
        get; set;
    }

    public string DbPath
    {
        get; set;
    }

    public BillContext(string dbPath = "billDb.db") => DbPath = dbPath;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + Path.Combine(AppContext.BaseDirectory, DbPath));
    }

}
