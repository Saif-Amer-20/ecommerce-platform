# Arabic E-Commerce Platform 🛍️# دليل تشغيل المشروع (باللغة العربية)



A comprehensive full-stack e-commerce platform designed specifically for Arabic markets, built with .NET 9.0 and Next.js. This platform supports right-to-left (RTL) languages, local payment methods, and Arabic business workflows.هذا المستودع يحتوي على حل متكامل لبناء متجر إلكتروني متكامل يعتمد على **.NET 8** في الخادم و**Next.js 14** في الواجهات، مع قاعدة بيانات PostgreSQL وRedis وMinIO وMeilisearch. يوضح هذا الدليل كيفية إعداد المشروع محليًا وتشغيله باستخدام Docker، بالإضافة إلى تشغيل الاختبارات ونشره.



![Build Status](https://img.shields.io/badge/build-passing-brightgreen)## المتطلبات الأساسية

![.NET Version](https://img.shields.io/badge/.NET-9.0-blue)

![Next.js](https://img.shields.io/badge/Next.js-14.0-black)* **Docker** و **Docker Compose** لتشغيل الخدمات في حاويات.

![License](https://img.shields.io/badge/license-MIT-green)* بديلًا للتطوير دون حاويات: تثبيت .NET 8 SDK وNode.js v20 وPostgreSQL وRedis وغيرها.



## 🌟 Features## هيكلية المشروع



### 🎯 Arabic-First Design```

- **Full RTL Support**: Complete right-to-left user interfaceecommerce/

- **Arabic Typography**: Proper Arabic font rendering and text flow ├── docs/               # وثائق مثل ERD وOpenAPI

- **Cultural Localization**: Designed for Arabic/Middle Eastern business practices ├── src/                # شيفرة الخادم (.NET)

- **Unicode Support**: Full Arabic character set and diacritics │   ├── Domain/

 │   ├── Application/

### 💼 Business Features │   ├── Infrastructure/

- **Multi-User System**: 12 different user types (Customer, Admin, Merchant, Courier, etc.) │   └── WebApi/

- **Local Payment Methods**: COD, ZainCash, AsiaHawala, Bank Transfer, Credit Card ├── apps/               # مشاريع الواجهة (Next.js)

- **Iraqi Dinar (IQD)**: Native currency support with proper formatting │   ├── storefront/

- **Arabic Address System**: City/Zone/Street format for regional addressing │   └── admin/

- **Courier Management**: Local delivery and logistics system ├── deploy/             # ملفات Docker وCompose

 ├── seeds/              # سكربتات إدخال البيانات الأولية

### 🏗️ Technical Architecture ├── tests/              # اختبارات (وحدة، تكامل، E2E)

- **Clean Architecture**: Domain-driven design with clear separation of concerns └── README.md

- **Microservices Ready**: Modular structure for scalability```

- **Entity Framework Core**: Modern ORM with Arabic data support

- **JWT Authentication**: Secure token-based authentication system## التشغيل المحلي باستخدام Docker

- **RESTful APIs**: Comprehensive REST API endpoints

1. تأكد من أن **Docker** يعمل.

## 🚀 Quick Start2. من داخل مجلد `ecommerce/deploy` شغّل:



### Prerequisites   ```bash

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)   docker-compose -f docker-compose.dev.yml up --build

- [Node.js 20+](https://nodejs.org/)   ```

- [Git](https://git-scm.com/)

   سيقوم هذا الأمر ببناء وتشغيل الخدمات التالية:

### Installation   - **api**: خادم .NET على المنفذ 8080.

   - **storefront**: واجهة المتجر على المنفذ 3000.

1. **Clone the repository**   - **admin**: لوحة الإدارة على المنفذ 3100.

   ```bash   - **db**: قاعدة بيانات PostgreSQL.

   git clone https://github.com/yourusername/arabic-ecommerce-platform.git   - **redis**: خادم Redis.

   cd arabic-ecommerce-platform   - **minio**: تخزين S3 متوافق على المنفذين 9000 و9001.

   ```   - **meili**: محرك البحث Meilisearch.



2. **Backend Setup (.NET API)**3. بعد تشغيل الخدمات، يمكنك الوصول إلى الواجهة عبر:

   ```bash   - المتجر: http://localhost:3000

   # Restore dependencies   - لوحة الإدارة: http://localhost:3100

   dotnet restore   - Swagger لواجهة API: http://localhost:8080/swagger (في بيئة التطوير)

   

   # Build the solution4. لإيقاف الخدمات:

   dotnet build

      ```bash

   # Run the API (choose one port)   docker-compose -f docker-compose.dev.yml down

   dotnet run --project src/WebApi/WebApi.csproj --urls="http://localhost:5000"   ```

   ```

## إعداد البيئة اليدوي (اختياري)

3. **Frontend Setup (Customer Storefront)**

   ```bash1. ثبّت **.NET 8 SDK** و**Node.js 20**.

   cd apps/storefront2. أنشئ قاعدة بيانات PostgreSQL وأعد ملف `docs/ddl.sql`:

   npm install

   npm run dev   ```bash

   # Runs on http://localhost:3000   psql -U postgres -d ecommerce -f docs/ddl.sql

   ```   ```



4. **Admin Panel Setup**3. شغّل الخادم:

   ```bash

   cd apps/admin   ```bash

   npm install   cd src/WebApi

   npm run dev -- --port 3001   dotnet run

   # Runs on http://localhost:3001   ```

   ```

4. شغّل واجهات Next.js:

### 🎉 Access Your Applications

   ```bash

| Application | URL | Description |   cd apps/storefront

|-------------|-----|-------------|   npm install

| **Customer Store** | http://localhost:3000 | Main shopping interface for customers |   npm run dev

| **Admin Dashboard** | http://localhost:3001 | Management panel for administrators |   ```

| **API Documentation** | http://localhost:5000/swagger | Interactive API documentation |

| **API Endpoints** | http://localhost:5000/api | RESTful API base URL |   وذات الشيء لـ `apps/admin`.



## 📚 Project Structure## المتغيرات والملفات المهمة



```- ملف `.env` غير موجود في المستودع، يجب إنشاءه وتحديد المتغيرات التالية في البيئة الإنتاجية:

arabic-ecommerce-platform/

├── src/                          # Backend .NET Solution  ```env

│   ├── Domain/                   # Domain entities and business logic  ASPNETCORE_ENVIRONMENT=Production

│   │   ├── Entities/            # Core business entities  JWT_SECRET=secret-key

│   │   │   ├── UserManagement.cs    # User, Role, Permission entities  DB__CONN=Host=db;Port=5432;Database=ecommerce;Username=postgres;Password=postgres

│   │   │   ├── OrderManagement.cs   # Order, Cart, Payment entities    REDIS__CONN=redis:6379

│   │   │   ├── CoreEntities.cs      # Product, Category entities  MINIO__ENDPOINT=http://minio:9000

│   │   │   └── InventoryManagement.cs # Inventory, Warehouse entities  MINIO__KEY=minioadmin

│   │   └── Domain.csproj  MINIO__SECRET=minioadmin

│   │  MEILI__HOST=http://meili:7700

│   ├── Application/              # Application services and interfaces  ZC__MERCHANT_ID=...

│   │   ├── Interfaces/          # Service contracts  ZC__API_KEY=...

│   │   │   ├── IUserService.cs      # User management interface  ZC__WEBHOOK_SECRET=...

│   │   │   ├── IOrderService.cs     # Order processing interface  # إلخ لباقي المزودين

│   │   │   └── ICatalogService.cs   # Product catalog interface  ```

│   │   └── Application.csproj

│   │- يمكنك تشغيل سكربت إدخال البيانات الأولية عبر:

│   ├── Infrastructure/           # Data access and external services

│   │   ├── Data/                # Entity Framework configuration  ```bash

│   │   │   ├── AppDbContext.cs      # Database context  psql -U postgres -d ecommerce -f seeds/seed_data.sql

│   │   │   └── Repositories/        # Data repositories  ```

│   │   ├── Services/            # Service implementations

│   │   │   ├── UserService.cs       # User management service## اختبار النظام

│   │   │   ├── OrderService.cs      # Order processing service

│   │   │   └── CatalogService.cs    # Catalog management service* **اختبارات الوحدات**: استخدم `dotnet test` داخل مجلد `src` لتشغيل اختبارات xUnit.

│   │   └── Infrastructure.csproj* **اختبارات الواجهة**: يمكن استخدام `npm test` لتشغيل اختبارات Vitest (غير مضمنة حاليًا).

│   │* **اختبارات End‑to‑End**: استخدم Playwright في مجلد `tests/e2e`:

│   └── WebApi/                   # API controllers and configuration

│       ├── Controllers/         # REST API controllers  ```bash

│       │   ├── UsersController.cs   # User authentication & management  npx playwright test ecommerce/tests/e2e/cod_purchase.spec.ts

│       │   ├── OrdersController.cs  # Order processing endpoints  ```

│       │   └── CatalogController.cs # Product catalog endpoints

│       ├── Program.cs           # Application entry point## نشر المشروع

│       ├── appsettings.json     # Configuration settings

│       └── WebApi.csproj        # Project dependenciesيتم إعداد **GitHub Actions** في `.github/workflows/ci.yml` لإجراء ما يلي عند كل دفع إلى الفرع `main`:

│

├── apps/                         # Frontend Applications1. بناء مشاريع .NET وNode وتشغيل الاختبارات.

│   ├── storefront/              # Customer-facing store (Next.js)2. بناء صور Docker: `ecommerce-api`, `ecommerce-storefront`, `ecommerce-admin`.

│   │   ├── src/app/            # App router pages3. يمكن إعداد خطوات إضافية لدفع الصور إلى Registry مثل Docker Hub أو GitHub Packages.

│   │   ├── components/         # Reusable UI components4. استخدام ملف `docker-compose.prod.yml` لتشغيل النسخة الإنتاجية. يتضمن خادم Proxy (Caddy) لتوجيه الطلبات إلى الخدمات المناسبة.

│   │   ├── styles/             # CSS and Tailwind styles

│   │   ├── tailwind.config.js  # Tailwind CSS configuration (RTL)## ملاحظات نهائية

│   │   └── package.json        # Frontend dependencies

│   │هذا المشروع يمثل هيكلًا أوليًا (MVP) يشتمل على الأساسيات فقط. يمكن تحسينه بإضافة مزيد من الخدمات (مثل التكامل مع بوابات الدفع الفعلية، إدارة السجلات المتقدمة، واجهات إدارة كاملة)، وزيادة مستوى الاختبارات لتغطية 60٪ من الشفرة على الأقل. تعتمد البنية على **Clean Architecture** لتسهيل إضافة المزايا المستقبلية وفصل المسؤوليات.
│   └── admin/                   # Administrative panel (Next.js)
│       ├── src/app/            # Admin dashboard pages
│       ├── components/         # Admin UI components
│       └── package.json        # Admin app dependencies
│
├── deploy/                       # Deployment configurations
│   ├── docker-compose.dev.yml  # Development containers
│   ├── docker-compose.prod.yml # Production containers
│   ├── Dockerfile.api          # API container setup
│   └── Dockerfile.frontend     # Frontend container setup
│
├── docs/                        # Documentation
│   ├── architecture.md         # System architecture overview
│   ├── ddl.sql                 # Database schema
│   ├── openapi.yaml           # API specification
│   └── postman_collection.json # API testing collection
│
└── seeds/                       # Sample data
    └── seed_data.sql           # Initial database seeding
```

## 🔧 API Endpoints

### Authentication & Users
```http
POST   /api/users/register        # Register new user with Arabic details
POST   /api/users/login           # User authentication
GET    /api/users/profile         # Get user profile
PUT    /api/users/profile         # Update user profile
GET    /api/users/addresses       # Get user addresses
POST   /api/users/addresses       # Add new address
```

### Product Catalog
```http
GET    /api/catalog/categories    # Get product categories
GET    /api/catalog/products      # Get products with Arabic search
GET    /api/catalog/products/{id} # Get product details
POST   /api/catalog/products      # Create product (Admin)
PUT    /api/catalog/products/{id} # Update product (Admin)
DELETE /api/catalog/products/{id} # Delete product (Admin)
```

### Order Management
```http
GET    /api/orders               # Get user orders
POST   /api/orders               # Create new order
GET    /api/orders/{id}          # Get order details
PUT    /api/orders/{id}/status   # Update order status (Admin)
POST   /api/orders/cart          # Manage shopping cart
```

## 🚀 Deployment

### Development Environment
```bash
# Start all services
docker-compose -f deploy/docker-compose.dev.yml up --build

# Or run individually:
dotnet run --project src/WebApi/WebApi.csproj --urls="http://localhost:5000" &
cd apps/storefront && npm run dev &
cd apps/admin && npm run dev -- --port 3001 &
```

### Production Deployment
```bash
# Build and deploy with Docker
docker-compose -f deploy/docker-compose.prod.yml up -d --build
```

## 🧪 Testing

### Backend Testing
```bash
# Run unit tests
dotnet test

# Run integration tests
dotnet test --filter "Category=Integration"
```

### Frontend Testing
```bash
# Run React component tests
cd apps/storefront
npm test

# Run end-to-end tests
npm run test:e2e
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Built with ❤️ for the Arabic e-commerce community**

*This platform is designed to empower Arabic businesses with modern, culturally-aware e-commerce solutions.*