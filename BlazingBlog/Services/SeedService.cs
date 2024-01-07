using BlazingBlog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazingBlog.Data.Entities;

namespace BlazingBlog.Services
{
	internal static class AdminAccount
	{
		public const string Name = "Nick Psaltakis";
		public const string Email = "nickpsal@gmail.com";
		public const string Role = "Admin";
		public const string Password = "gbrCBRM2908!@34";
	}

	public interface ISeedService
	{
		Task SeedDataAsync();
	}

	public class SeedService : ISeedService
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IUserStore<ApplicationUser> _UserStore;
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public SeedService(ApplicationDbContext dbContext, IUserStore<ApplicationUser> UserStore, UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> roleManager)
		{
			_dbContext = dbContext;
			_UserStore = UserStore;
			_UserManager = UserManager;
			_roleManager = roleManager;
		}

		private async Task MigrateDatabaseAsync()
		{
			#if DEBUG
						if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
						{
							await _dbContext.Database.MigrateAsync();
						}

			#endif
		}

		public async Task SeedDataAsync()
		{
			//check Migration
			await MigrateDatabaseAsync();
			//Seed Admin Role
			if (await _roleManager.FindByNameAsync(AdminAccount.Role) is null)
			{
				//dont exists in DB
				var AdminRole = new IdentityRole(AdminAccount.Role);
				var result = await _roleManager.CreateAsync(AdminRole);
				if (!result.Succeeded)
				{
					var errorsString = result.Errors.Select(e => e.Description);
					throw new Exception($"Error in Creating Admin Role : {Environment.NewLine} {string.Join(Environment.NewLine, errorsString)}");
				}
			}
			//Seed Admin User
			var adminUser = await _UserManager.FindByEmailAsync(AdminAccount.Email);
			if (adminUser is null)
			{
				//dont exists in DB
				adminUser = new ApplicationUser();
				adminUser.Name = AdminAccount.Name;
				await _UserStore.SetUserNameAsync(adminUser, AdminAccount.Email, CancellationToken.None);
				var emailStore = (IUserEmailStore<ApplicationUser>)_UserStore;
				await emailStore.SetEmailAsync(adminUser, AdminAccount.Email, CancellationToken.None); 
				var result = await _UserManager.CreateAsync(adminUser, AdminAccount.Password);
				if (!result.Succeeded)
				{
					var errorsString = result.Errors.Select(e => e.Description);
					throw new Exception($"Error in Creating Admin User : {Environment.NewLine} {string.Join(Environment.NewLine, errorsString)}");
				}else
				{
					await _UserManager.AddToRoleAsync(adminUser, AdminAccount.Role);
				}
			}
			//Seed Catogories
			if (!await _dbContext.Categories.AsNoTracking().AnyAsync())
			{
				//no categorires in the Db
				await _dbContext.Categories.AddRangeAsync(Category.GetSeedCategories());
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
