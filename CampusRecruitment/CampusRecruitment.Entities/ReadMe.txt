
Scaffold-DbContext "Server=20.191.102.175,1433;Database=TechathonDB_user8;User Id=techathondbuser8;Password=wr5drLc#;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities

Database Migration using code first approach script

add-migration InitialMigration -Context CampusRecruitmentContext
update-database  -Context CampusRecruitmentContext