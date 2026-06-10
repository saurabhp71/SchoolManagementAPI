namespace SchoolManagement.API.Extensions
{
    public static class MiddlewareConfiguration
    {
        public static WebApplication UseApplicationMiddleware(
            this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
