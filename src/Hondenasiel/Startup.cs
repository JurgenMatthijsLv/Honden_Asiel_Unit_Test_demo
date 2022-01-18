using Hondenasiel.Application.Commands;
using Hondenasiel.Application.Queries;
using Hondenasiel.Common;
using Hondenasiel.Controllers;
using Hondenasiel.Infrastructure.Database;
using Hondenasiel.Infrastructure.Webapi;
using Hondenasiel.InfrastructureFilesystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.IO.Abstractions;

namespace Hondenasiel
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMediatrOnUseCases();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hondenasiel", Version = "v1" });
				c.OperationFilter<SwaggerFileOperationFilter>();
			});

			services.AddDbContext<HondenasielDbContext>(opt =>
			{
				opt.UseSqlServer(Configuration.GetConnectionString(Constants.HondenasielDbContext)).EnableSensitiveDataLogging(true);
			});

			//Repositories
			services.AddTransient<ICommandAsielRepository, CommandAsielRepository>();
			services.AddTransient<IQueryRefRepository, QueryRefRepository>();
			services.AddTransient<IHondDtoRepository, HondDtoRepository>();
			services.AddTransient<IFotoRepository, FotoRepository>();
			services.AddTransient<IFileSystem, FileSystem>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => {
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hondenasiel v1");
				});
			}

			app.ConfigureExceptionHandler();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}