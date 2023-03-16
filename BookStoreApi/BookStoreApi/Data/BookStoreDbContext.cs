using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookStoreApi.Data
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
		
		public DbSet<Book> Books { get; set;}
		public DbSet<Author> Authors { get; set; }
		public DbSet<BookCopy> BookCopies { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<EBook> EBooks { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Section> Sections { get; set; }

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
		}

		public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
		{
			/// <summary>
			/// Creates a new instance of this converter.
			/// </summary>
			public DateOnlyConverter() : base(
					d => d.ToDateTime(TimeOnly.MinValue),
					d => DateOnly.FromDateTime(d))
			{ }
		}
	}
}
