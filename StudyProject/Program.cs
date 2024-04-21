using Microsoft.EntityFrameworkCore;
using StudyProject;
using StudyProject.Controllers;
using StudyProject.Models;
using StudyProject.Repositories;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectManagementContext>(op =>
{
    op.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
    new MySqlServerVersion(new Version(10, 4, 32)));
});

builder.Services.AddScoped<ProjectManagementContext>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CommentRepository, CommentRepository>();
builder.Services.AddScoped<MemberRepository, MemberRepository>();
builder.Services.AddScoped<ProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ReportRepository, ReportRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
builder.Services.AddScoped<StatusRepository, StatusRepository>();
builder.Services.AddScoped<StudentRepository, StudentRepository>();
builder.Services.AddScoped<TaskAssignmentRepository, TaskAssignmentRepository>();
builder.Services.AddScoped<TaskRepository, TaskRepository>();
builder.Services.AddScoped<UserRepository, UserRepository>();

builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoriesController>();
builder.Services.AddScoped<CommentsController>();
builder.Services.AddScoped<MembersController>();
builder.Services.AddScoped<ProjectsController>();
builder.Services.AddScoped<ReportsController>();
builder.Services.AddScoped<RolesController>();
builder.Services.AddScoped<StatusController>();
builder.Services.AddScoped<StudentsController>();
builder.Services.AddScoped<TaskAssignmentsController>();
builder.Services.AddScoped<TasksController>();
builder.Services.AddScoped<UsersController>();

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
