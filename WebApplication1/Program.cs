var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddAntiforgery(); // 可加強明確性

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<OrderHub>("/orderHub");

// ? 加這段，讓 "/" 自動導向 "/Order/aaa"
app.MapGet("/", context =>
{
    context.Response.Redirect("/Order/aaa");
    return Task.CompletedTask;
});

app.Run();