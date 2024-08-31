# Blog_App
#This application is made by using .Net Core MVC and SQL SERVER and Entity framework-code first approach.
#For the code-first approach we used Add-Migration and Update-Database commands to create a database based on Models.
#In this app we can upload new blog posts view all blog posts and use cards to show blog posts as well

create entities and db context class and configure connection string as well and use below commands

Migration Command
Enable migrations -
Enable-Migrations

Create migration -
Add-Migration MigrationsName

Create upgrade/downgrade script -
Update-Database

Step to Setup Project

-Create Dot net Core MVC Project
-Install EntitiyFramework nuget packages - Microsoft.EntityFramework.Tools , Microsoft.EntityFrameWork.SqlServer
-Setup Models for your Projects
-Configure/Define Database connection String into appsetings.json
-Create/Define DbContext files
-configure DB connection configuration in program.cs file and AddMvc() middleware as well
-Then use migration cmd to create tables - Add-Migration Test , Update-Database
-Then Create Controller with EntityFrameWork read /write and select model and context file


