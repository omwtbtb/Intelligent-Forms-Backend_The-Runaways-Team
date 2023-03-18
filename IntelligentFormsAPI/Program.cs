using FluentValidation;
using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Mapper;
using IntelligentFormsAPI.Application.Models;
using IntelligentFormsAPI.Application.Models.FluentValidation;
using IntelligentFormsAPI.Application.Services;
using IntelligentFormsAPI.Infrastructure.Contexts;
using IntelligentFormsAPI.Infrastructure.Interfaces;
using IntelligentFormsAPI.Infrastructure.Quartz;
using IntelligentFormsAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<ISubmissionsRepository, SubmissionsRepository>();
builder.Services.AddScoped<ISubmissionsService, SubmissionsService>();

builder.Services.AddScoped<IFormsRepository, FormsRepository>();
builder.Services.AddScoped<IFormsService, FormsService>();

builder.Services.AddScoped<IFormsScannerService, FormsScannerService>();

builder.Services.AddScoped<IValidator<UserSignUpDto>, UserSignUpValidator>();


builder.Services.AddDbContext<EFContext>(options =>
    options.UseCosmos("https://f447355d-0ee0-4-231-b9ee.documents.azure.com:443/",
    "FSQsotIEON6q0I18CON0jelb5ZJFXxkfoVNvWde9FfBQxO0o5pmvgUaRLXgFYIMOvS8Eh8EG2ViJACDbGZaloA==",
    "IntelligentFormsDB"));

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("SendReminderEmailJob");

    q.AddJob<DeleteOldSubmissionsJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DeleteOldSubmissionsJob-trigger")
        .WithCronSchedule("0 0 0 ? * *"));

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(FormProfile));
builder.Services.AddAutoMapper(typeof(SubmissionProfile));


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseSwagger();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();


app.Run();
