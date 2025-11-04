# Technical-Assessment-Employee-Leave-Request


<div align="center">
    <img src="leavesystem.png" alt="Logo" width="250"/>
</div>

# Leave System 
Check out the live site here: [Visit Site Leave System](https://leavesystemweb-fqadejbnh6a7frd0.uaenorth-01.azurewebsites.net/)
## 💡Project Description
The **Leave Management System** is a comprehensive web-based platform designed to streamline and automate the process of managing employee leave requests within an organization. The system allows employees to submit leave requests, track their status, and view their leave history, while managers can review, approve, or reject requests.

## Key Functionalities

 1. User Registration & Role Assignment
- Employees and managers can create accounts.
- Users are assigned roles with specific permissions.

2. Leave Request Management
- Employees can submit leave requests specifying:
  - **Start Date**
  - **End Date**
  - **Reason** for the leave

3. Dashboard

#### Employee Dashboard
- Displays submitted leave requests with **status tracking**.
- Employees can **delete pending requests**.
- Visual **leave chart** to show approved vs rejected leaves.

#### Manager Dashboard
- Shows all employee leave requests.
- Includes **filtering and search** by employee name.
- Managers can **approve or reject pending requests**.

### 4. Authentication & Security
- Secure login using **email and password**.
- **Role-based access control** ensures proper authorization.

 Purpose
The system aims to **improve transparency, reduce paperwork, and enhance efficiency** in managing employee leaves, providing both employees and managers with a **user-friendly interface** and **real-time insights**.



## 📑 Mockup
To visualize the user interface and workflow, refer to the [Mockup PDF](https://drive.google.com/file/d/12wB1cyK-QHAHB5GA0WrLVicrBg6D0T1G/view?usp=sharing) file for a detailed view of the application screens and features.


## 💻 System Requirements(For Development)

| Icon                                                                                             | Software                          | Version              | Description                                                                                                                    |
|--------------------------------------------------------------------------------------------------|-----------------------------------|----------------------|-------------------------------------------------------------------------------------------------------------------------------|
| ![SQL Server](https://img.icons8.com/?size=50&id=laYYF3dV0Iew&format=png&color=000000)                                             | **SQL Server**                    | 2022                 | Required to create and manage the server database.                                                                           |
| ![SQL Server](https://img.utdstc.com/icon/981/2d8/9812d89705787310adf08f0edf758921b8d551e8329c8d8c5eeabf4d06b08378:40)                                            | **SQL Server Management Studio**  | `v20.2`       | To access and manage tables.                                                                |
| ![Visual Studio](https://skillicons.dev/icons?i=visualstudio)                                   | **Visual Studio**                 | 2022                 | Required for creating API services with .NET tools and extensions.                                                           |


## 🌐 FrameWorks And Dependencies

| Icon                                                                                     | Language/Technology               | Version              | Description                                                    |
|------------------------------------------------------------------------------------------|-----------------------------------|----------------------|----------------------------------------------------------------|
|  ![dotnet](https://skillicons.dev/icons?i=dotnet)   | **.NET**                          | `8.0`                    | Used for Pages development.                              |
|  ![SQL Server](https://img.icons8.com/?size=50&id=laYYF3dV0Iew&format=png&color=000000)   | **SQL Server**                   | 2022                 | Database management system for storing application data.       |

## 📥 Installation Instructions

### 1. First Time Installation

If this is the first time installing the required software, follow these steps:

1. **Download SQL Server 2022**:
   - Visit the official Microsoft website: [Download SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
   - Scroll Down The Page and Click (Download now) with Developer.
   - Follow the installation prompts to install SQL Server.

2. **Download SQL Server Management Studio (SSMS)**:
   - Go to the [SSMS Download Page](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
   -Scroll Down and Click on the download link (Download SQL Server Management Studio (SSMS) 20.2) and follow the installation instructions.

3. **Download Visual Studio 2022**:
   - Visit the [Visual Studio Download Page](https://visualstudio.microsoft.com/vs/)
   - Choose the Community version (free for individual developers) and install it.
   - when install Chooce in **Workloads** `ASB.NET and web development` and click with **Individual components** chooce `.NET 8.0 RunTime` and `.NET 8.0 WebAssembly` and click install.

### 2. If Software is Already Installed

If you already have the required software installed, ensure they are updated to the latest versions:

- <span style="color:#03e3fc; font-size:22px;">**SSMS**</span> : 
1. Create Database and extract the connection string and set the link in the API project.
2. Use the query to get the connection string, taking into account the input so that it includes userid, password if you need them to log in to the server.
3. The script checks if a `User ID` and `Password` are provided:
   - If **both** are provided, it uses **SQL Server Authentication**.
   - If **neither** are provided, it defaults to **Windows Authentication** with `Trusted_Connection=True`.

4. Please change the connection string in the appsettings file to the corresponding one (local db / remote db).
- <span style="color:#03e3fc; font-size:22px;">**Visual Studio**</span>: 
1. Open Visual Studio.
2. Go to **File** > **Open** > **Folder...** and select the project folder, or right-click the folder and choose **Open in Visual Studio**.

   ##### Setting the Connection String

3. Once the project is open, locate the `appsettings.json` file in the project root.
4. Open `appsettings.json` and set the `ConnectionStrings` property by adding your database connection string. For example:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your_Connection_String_Here"
       //delete this and set the connection string with extract mssql
     }
   }
   ```
   ##### Add Migrations Using Entity Framework
5. Click with Tools in tool bar in visual studio and click with  **Nuget Package Manager** and click **Package Manager Console** with open command 
write the command this 
   - ``add-migration Initial_Migration -Project LeaveSystem.Infrastructure -StartupProject LeaveSystem.UI``.
   - this command create migration with DB create tables

6. in the end Run This Projects click **ctrl+F5** or **Run Icon**





### 🚀 Enjoy Using the Leave System!


## 📞 Contact

For any questions or support, please feel free to reach out:

- **Email**: [abdtawil25@gmail.com)](mailto:abdtawil25@gmail.com)
- **Phone**: +962795072791
- **LinkedIn**: [linkedin](https://linkedin.com/in/abdullabataineh/)
- **GitHub**: [github](https://github.com/Abdullah-Bataineh)

We look forward to assisting you!


