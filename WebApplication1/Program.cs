var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddAntiforgery(); // �i�[�j���T��

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

// ? �[�o�q�A�� "/" �۰ʾɦV "/Order/aaa"
app.MapGet("/", context =>
{
    context.Response.Redirect("/Order/aaa");
    return Task.CompletedTask;
});

app.Run();