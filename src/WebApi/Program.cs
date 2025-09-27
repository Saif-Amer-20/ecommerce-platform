using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Data.Repositories;
using Ecommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// قراءة الإعدادات من appsettings والبيئة
var configuration = builder.Configuration;

// إضافة DbContext - استخدام In-Memory لأغراض التجربة
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("EcommerceDb");
});

// إضافة المستودعات والخدمات
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// إضافة خدمات الأعمال (Business Services)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();

// إعداد المصادقة والتخويل
var jwtSecret = configuration["JWT_SECRET"] ?? "development-secret-key-very-long-for-testing";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero
        };
    });

// إضافة سياسات التخويل القائمة على الأذونات
builder.Services.AddAuthorization(options =>
{
    // Customer Policies
    options.AddPolicy("RequireCustomer", policy => policy.RequireRole(UserTypes.Customer));
    
    // Catalog Management Policies
    options.AddPolicy("RequireCategoriesRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.CategoriesRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireCategoriesWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.CategoriesWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireCategoriesDelete", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.CategoriesDelete) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireProductsRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.ProductsRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireProductsWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.ProductsWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireProductsDelete", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.ProductsDelete) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireAttributesRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.AttributesRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireAttributesWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.AttributesWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequirePricingWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.PricingWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    // Order Management Policies
    options.AddPolicy("RequireOrdersRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.OrdersRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireOrdersWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.OrdersWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireShipmentsRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.ShipmentsRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireShipmentsWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.ShipmentsWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireShipmentsUpdateStatus", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.ShipmentsUpdateStatus) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    // User & Role Management Policies
    options.AddPolicy("RequireUsersRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.UsersRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireUsersWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.UsersWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireRolesRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.RolesRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequireRolesWrite", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.RolesWrite) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    options.AddPolicy("RequirePermissionsRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.PermissionsRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    // Analytics & Reports
    options.AddPolicy("RequireAnalyticsRead", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("permission", SystemPermissions.AnalyticsRead) ||
        context.User.HasClaim("permission", SystemPermissions.All)));
    
    // Courier Specific
    options.AddPolicy("RequireCourier", policy => policy.RequireRole(UserTypes.Courier));
});

// إضافة Controllers
builder.Services.AddControllers();

// إضافة Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Arabic E-Commerce API - نظام التجارة الإلكترونية", 
        Version = "v1",
        Description = @"
## نظام التجارة الإلكترونية العربي المتكامل
### Arabic E-Commerce System with RBAC

**العملة:** الدينار العراقي (IQD)  
**اللغة:** العربية (RTL)  
**نظام الأذونات:** RBAC (Role-Based Access Control)

### أنواع المستخدمين:
- 👤 **الضيوف (Guest):** تصفح المنتجات وإنشاء الحسابات
- 🛍️ **العملاء (Customer):** إدارة الحساب والطلبات
- 🧑‍💼 **المدير العام (Super Admin):** إدارة كامل النظام
- 📦 **مدير الكتالوج (Catalog Manager):** إدارة المنتجات والفئات
- 🚚 **مدير العمليات (Operations Manager):** إدارة الطلبات والشحن
- 🏷️ **مدير المستودع (Warehouse Manager):** إدارة المخزون
- 🛵 **عامل التوصيل (Courier):** تنفيذ التوصيل
- 💰 **المالية (Finance):** إدارة المدفوعات والتسوية
- 🎧 **دعم العملاء (Support):** خدمة العملاء
- 📈 **التسويق (Marketing):** إدارة الحملات والعروض

### طرق الدفع المدعومة:
- 💵 **الدفع عند الاستلام (COD)**
- 📱 **زين كاش (ZainCash)**  
- 🏦 **آسيا حوالة (AsiaHawala)**
- 💳 **التحويل المصرفي**

### الميزات:
- ✅ نظام أذونات متقدم (RBAC)
- ✅ دعم كامل للعربية (RTL)
- ✅ إدارة المخزون المتقدمة
- ✅ تتبع الطلبات والشحن
- ✅ نظام المراجعات والتقييمات
- ✅ دعم المتغيرات والخصائص
- ✅ تقارير مفصلة وتحليلات
"
    });
    
    // إضافة JWT Security Definition
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "أدخل التوكن بصيغة: Bearer {token}"
    });
    
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// إضافة CORS للدعم الواجهات الأمامية
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3001", "http://localhost:3100") // Storefront & Admin
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// تفعيل CORS
app.UseCors("AllowFrontend");

// تفعيل Swagger (enabled for all environments for demo purposes)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Arabic E-Commerce API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "نظام التجارة الإلكترونية العربي - Arabic E-Commerce API";
});

app.UseAuthentication();
app.UseAuthorization();

// تسجيل Controllers
app.MapControllers();

// تعريف Endpoints أساسية

app.MapGet("/", () => new
{
    message = "🛍️ نظام التجارة الإلكترونية العربي يعمل بنجاح!",
    englishMessage = "Arabic E-Commerce API is running successfully!",
    version = "v1.0",
    currency = "IQD",
    language = "ar",
    direction = "rtl",
    swagger = "/swagger",
    endpoints = new
    {
        users = "/api/v1/users",
        orders = "/api/v1/orders", 
        catalog = "/api/v1/catalog"
    }
});

// Basic Auth endpoints (simplified)
app.MapPost("/api/v1/auth/register", (RegisterRequest request) =>
{
    // تنفيذ مبسط لإنشاء الحساب
    return Results.Created("/api/v1/users/profile", new 
    { 
        message = "تم إنشاء الحساب بنجاح. يرجى التحقق من بريدك الإلكتروني.",
        userId = new Random().Next(1000, 9999),
        email = request.Email
    });
});

app.MapPost("/api/v1/auth/login", (LoginRequest request) =>
{
    // تنفيذ مبسط لتسجيل الدخول
    var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"demo-token-{request.Email}"));
    return Results.Ok(new 
    { 
        message = "تم تسجيل الدخول بنجاح",
        accessToken = token,
        expiresIn = 3600,
        userType = "customer"
    });
});

app.Run();

// DTOs للنقاط النهائية الأساسية
public record RegisterRequest(string Email, string Password, string FullName, string? Phone);
public record LoginRequest(string Email, string Password);