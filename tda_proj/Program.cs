global using tda_proj.Model;
global using tda_proj.Data;
global using tda_proj.Service;
global using tda_proj.Service.LectorServiceApi;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//Swagger (nefunguje)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ILectorServiceApi, LectorServiceApi>();
builder.Services.AddDbContext<tdaContext>();




// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();


}



// Set the provider
SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Use CORS
app.UseCors("AllowAny");

// Map API controllers
app.MapControllers();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();



