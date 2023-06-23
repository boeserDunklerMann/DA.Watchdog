How to create these classes:
in Package Manager Console paste this:
Scaffold-DbContext "Database=Watchdog; Trusted_Connection=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -Force
