# ExchangeGateway

**دروازهٔ پرداخت و پایش تراکنش‌های ارز دیجیتال (Bitcoin & Tron/TRC20)** — سرویسی مبتنی بر .NET برای شناسایی خودکار واریزی‌های کریپتویی و اطلاع‌رسانی آن‌ها از طریق Webhook.

---

## 📖 دربارهٔ پروژه

ExchangeGateway یک بک‌اند لایه‌بندی‌شده به زبان C# / .NET است که به‌عنوان **گیت‌وی پرداخت ارز دیجیتال** عمل می‌کند. سرویس با پایش مداوم آدرس‌های کیف‌پول روی شبکه‌های **Bitcoin** و **Tron (TRC20 / USDT)**، واریزی‌های جدید را به‌صورت خودکار شناسایی کرده و نتیجه را از طریق **Webhook** به سیستم مقصد اطلاع می‌دهد.

پروژه از دو بخش اصلی تشکیل شده:

- **ExchangeGateway (EG.Web)** — سرویس وب مبتنی بر ASP.NET Core که واسط API و دسترسی به داده را فراهم می‌کند.
- **EG.Bot** — سرویس پس‌زمینه (Worker/Scheduler) که به‌صورت زمان‌بندی‌شده تراکنش‌های شبکه‌های بلاکچین را استعلام و پردازش می‌کند.

---

## ✨ امکانات

- 🔍 پایش خودکار آدرس‌های کیف‌پول Bitcoin و Tron برای شناسایی واریزی جدید
- 🔗 استعلام تراکنش‌ها از طریق API های بلاکچین (Bitcoin API و TronScan API)
- ⏱️ اجرای زمان‌بندی‌شده (Scheduled Job) برای بررسی دوره‌ای تراکنش‌ها
- 📤 اطلاع‌رسانی نتیجهٔ تراکنش‌ها به سیستم‌های بیرونی از طریق Webhook
- 💼 مدیریت کیف‌پول‌ها و درخواست‌های فروش (Sell Request)
- 🧾 ثبت ورودی/خروجی تراکنش‌های بیت‌کوین بر پایهٔ مدل UTXO (Vin/Vout)
- 🗂️ ثبت و پیگیری خطاها (Error Logging)
- 🏗️ معماری لایه‌بندی‌شده و تفکیک مسئولیت‌ها (Clean/Layered Architecture)

---

## 🏗️ معماری و ساختار پروژه

```
ExchangeGateway/
├── ExchangeGateway/       # لایهٔ Web (ASP.NET Core) — نقطهٔ ورود API
├── EG.Bot/                # سرویس پس‌زمینه با Scheduled Job برای پایش تراکنش‌ها
│   └── Schedules/         # وظایف زمان‌بندی‌شده (مثل GetTransactionSchedule)
├── EG.Business/           # منطق کسب‌وکار
│   ├── Apis/               # ارتباط با API های بیرونی (Bitcoin, TronScan, Webhook)
│   └── Services/           # سرویس‌های Business (Bitcoin, Tron, Wallet, Webhook, ...)
├── EG.Repository/         # لایهٔ دسترسی به داده (EF Core)
│   ├── Domain/              # Entity ها و DataContext
│   ├── Migrations/          # مایگریشن‌های دیتابیس
│   └── Services/            # پیاده‌سازی Repository ها
├── EG.Model/               # مدل‌ها و DTO ها
│   ├── DTO/
│   └── General/
├── EG.Config/              # پیکربندی AutoMapper و Dependency Injection
├── EG.General/              # ابزارهای عمومی، Enum ها و Helper ها
└── ExchangeGateway.sln
```

این ساختار از یک معماری چندلایه‌ی رایج در پروژه‌های سازمانی .NET پیروی می‌کند و مسئولیت‌ها به‌وضوح بین لایه‌های **Presentation (Web/Bot)**، **Business**، **Repository/Data Access** و **Model/Shared** تقسیم شده‌اند.

---

## 🧱 پشته‌ی فناوری (Tech Stack)

| بخش | فناوری |
|---|---|
| زبان / فریم‌ورک | C# / .NET |
| وب سرویس | ASP.NET Core |
| دسترسی به داده | Entity Framework Core |
| دیتابیس | SQL Server |
| نگاشت آبجکت | AutoMapper |
| بلاکچین | Bitcoin API، TronScan API |
| اطلاع‌رسانی | Webhook |

---

## 🗄️ موجودیت‌های اصلی دیتابیس

| Entity | توضیح |
|---|---|
| `TblWallet` | اطلاعات کیف‌پول‌های تحت پایش |
| `TblBitcoin` | تراکنش‌های شبکهٔ Bitcoin |
| `TblVinBitcoin` / `TblVoutBitcoin` | ورودی و خروجی تراکنش بیت‌کوین (مدل UTXO) |
| `TblTron` | تراکنش‌های شبکهٔ Tron (TRC20) |
| `TblSellRequest` | درخواست‌های فروش ارز دیجیتال |
| `TblWebhookRequest` | لاگ درخواست‌های ارسال‌شده به Webhook |
| `TblError` | لاگ خطاهای رخ‌داده در سیستم |

---

## ⚙️ پیش‌نیازها

- [.NET SDK](https://dotnet.microsoft.com/download) (نسخهٔ سازگار با پروژه)
- SQL Server (یا LocalDB برای توسعه)
- دسترسی به API های بلاکچین مورد استفاده (Bitcoin / TronScan)

## 🚀 راه‌اندازی

```bash
# کلون پروژه
git clone https://github.com/DiakoHasani/ExchangeGateway.git
cd ExchangeGateway

# ریستور پکیج‌ها
dotnet restore

# تنظیم Connection String دیتابیس در appsettings.json
# (کلید defaultConnection در پروژه‌ی ExchangeGateway)

# اجرای مایگریشن‌ها
dotnet ef database update --project EG.Repository --startup-project ExchangeGateway

# اجرای سرویس وب
dotnet run --project ExchangeGateway

# اجرای بات پایش تراکنش‌ها (در ترمینال جداگانه)
dotnet run --project EG.Bot
```

> ⚠️ پیش از اجرا، مقادیر لازم مانند Connection String دیتابیس و کلیدهای دسترسی به API های بلاکچین را در فایل‌های `appsettings.json` هر پروژه تنظیم کنید.

---

## 🔄 جریان کار (Workflow)

1. یک آدرس کیف‌پول برای پایش در سیستم ثبت می‌شود (`TblWallet`).
2. `EG.Bot` به‌صورت زمان‌بندی‌شده (`GetTransactionSchedule`) تراکنش‌های جدید را از طریق `BitcoinApi` / `TronScanApi` استعلام می‌کند.
3. تراکنش‌های شناسایی‌شده در دیتابیس ثبت می‌شوند (`BitcoinBusiness`, `TronBusiness`, `WalletBusiness`).
4. نتیجه از طریق `WebhookApi` به آدرس مقصد تعریف‌شده ارسال می‌شود.
5. خطاهای احتمالی در هر مرحله توسط `ErrorBusiness` ثبت می‌گردند.

---

## 🤝 مشارکت

Pull Request ها و Issue های شما برای بهبود پروژه خوش‌آمد هستند.

## 📄 لایسنس

این پروژه در حال حاضر بدون لایسنس مشخص منتشر شده است. در صورت نیاز به استفاده یا توزیع، با نویسندهٔ پروژه هماهنگ کنید.
