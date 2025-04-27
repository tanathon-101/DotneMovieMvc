# 🎬 MovieMvc

โปรเจกต์ตัวอย่าง ASP.NET Core MVC สำหรับจัดการข้อมูลภาพยนตร์  
รองรับการอัปโหลดภาพ, บันทึกข้อมูลหนัง, แก้ไข, ลบ และแสดงรายการหนัง

---

## 🚀 Tech Stack

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Bootstrap 4.6
- Tempus Dominus DateTimePicker
- jQuery & Moment.js

---

## 📦 Features

- [x] CRUD ภาพยนตร์ (Create, Read, Update, Delete)
- [x] อัปโหลดรูปภาพ (Upload Image)
- [x] ปฏิทิน DateTime Picker สวยงาม
- [x] Toast Notification แจ้งเตือนสำเร็จ
- [x] Validation ฟอร์ม ทั้งฝั่ง Client และ Server
- [x] แสดงรูป Cover เป็น Base64 หรือไฟล์
- [x] UI Bootstrap Responsive

---

## 🛠 Database Schema (Table: Movies)

```sql
CREATE TABLE Movies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    CoverImg NVARCHAR(MAX) NULL, -- base64 image or path
    ReleaseDate DATETIME NOT NULL,
    Genre NVARCHAR(100) NOT NULL,
    Duration INT NOT NULL,
    CreateDate DATETIME NULL,
    ModifyDate DATETIME NULL
);
