using Microsoft.EntityFrameworkCore;

namespace RecallFinanceApp.Models;

public class RecallDbContext : DbContext
{
    public RecallDbContext(DbContextOptions<RecallDbContext> options) : base(options) { }

}