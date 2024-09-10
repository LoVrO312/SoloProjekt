using Autofac;
using Autofac.Extensions.DependencyInjection;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service;
using Introduction.Service.Common;
//using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(ContainerBuilder =>
{
    ContainerBuilder.RegisterType<SubjectService>().As<ISubjectService>().InstancePerDependency();
    ContainerBuilder.RegisterType<SubjectRepository>().As<ISubjectRepository>().InstancePerDependency();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
