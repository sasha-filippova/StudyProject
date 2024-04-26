using StudyProject.Repositories;

namespace StudyProject
{
    public static class RepositoryExten
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ProjectManagementContext>();
            services.AddScoped<CommentRepository, CommentRepository>();
            services.AddScoped<MemberRepository, MemberRepository>();
            services.AddScoped<ProjectRepository, ProjectRepository>();
           services.AddScoped<ReportRepository, ReportRepository>();
            services.AddScoped<RoleRepository, RoleRepository>();
            services.AddScoped<StatusRepository, StatusRepository>();
            services.AddScoped<StudentRepository, StudentRepository>();
            services.AddScoped<TaskAssignmentRepository, TaskAssignmentRepository>();
                services.AddScoped<TaskRepository, TaskRepository>();
            services.AddScoped<UserRepository, UserRepository>();

            services.AddScoped<CategoryRepository, CategoryRepository>();

        }
    }
}
