using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PurchasingTask.Data;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;
using PurchasingTask.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Vendor, IdentityRole>(
	options =>
	{
		//temp for dev
		options.Password.RequiredUniqueChars = 0;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;
		options.Password.RequiredLength = 8;
		options.Password.RequireNonAlphanumeric = false;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new[] { "Admin", "Vendor" };
	foreach (var role in roles)
	{
		if (!await _roleManager.RoleExistsAsync(role))
		{
			await _roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	if (!context.PaymentMethods.Any())
	{
		context.PaymentMethods.AddRange(new List<PaymentMethod>
		{
			new PaymentMethod { PaymentMethodName = "Credit Card" },
			new PaymentMethod { PaymentMethodName = "PayPal" }
		});
		await context.SaveChangesAsync();
	}
}

	using (var scope = app.Services.CreateScope())
{
	var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<Vendor>>();
	var name = "Admin";
	var email = "Admin@adm.com";
	var password = "Admin@1234";
	if (await _userManager.FindByEmailAsync(email) == null)
	{
		var vendorAdmin = new Vendor
		{
			UserName = name,
			VendorName = name,
			VendorAddress = "here",
			Email = email,
			PaymentMethodId = 1,
		};
		await _userManager.CreateAsync(vendorAdmin, password);
		await _userManager.AddToRoleAsync(vendorAdmin, "Admin");
	}
}

app.Run();
