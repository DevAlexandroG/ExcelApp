using ExcelApp.Interfaces;
using ExcelApp.Services;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddSingleton<TableService>();
// builder.Services.AddSingleton<ExcelService>();
builder.Services.AddScoped<IDataMergerService, DataMergerService>();
builder.Services.AddScoped<IExcelReader, ExcelReader>();
builder.Services.AddScoped<ProductInfoFactory>();
builder.Services.AddScoped<IFileFormatValidator, XlsxFileFormatValidator>();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


// Create the logger factory with Serilog

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();