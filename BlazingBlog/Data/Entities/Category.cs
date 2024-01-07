using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace BlazingBlog.Data.Entities
{
	public class Category
	{
		public short Id { get; set; }
		[Required, MaxLength(50)]
		public string Name { get; set; }
		[MaxLength(75)]
		public string Slug { get; set; }
		public bool ShowInNavBar { get; set; }

		public static Category[] GetSeedCategories()
		{
			return
			[
				new Category { Name = "C#", Slug = "c-sharp", ShowInNavBar = true },
				new Category { Name = "ASP.NET", Slug = "asp-net-core", ShowInNavBar = true },
				new Category { Name = "Blazor", Slug = "blazor", ShowInNavBar = true },
				new Category { Name = "SQL Server", Slug = "sql-server", ShowInNavBar = false },
				new Category { Name = "Entity Framework Core", Slug = "entity-framework-core", ShowInNavBar = true },
				new Category { Name = "Angular", Slug = "angular", ShowInNavBar = false },
				new Category { Name = "React", Slug = "react", ShowInNavBar = false },
				new Category { Name = "Vue", Slug = "vue", ShowInNavBar = true },
				new Category { Name = "Javascript", Slug = "javascript", ShowInNavBar = false },
				new Category { Name = "HTML", Slug = "html", ShowInNavBar = false },
				new Category { Name = "CSS", Slug = "css", ShowInNavBar = false },
				new Category { Name = "Bootstrap", Slug = "bootstrap", ShowInNavBar = false },
				new Category { Name = "MVC", Slug = "mvc", ShowInNavBar = true }
			];
		}
	}
}
