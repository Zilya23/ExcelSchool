﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExcelTestForSchool.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class School6Entities1 : DbContext
    {
        public School6Entities1()
            : base("name=School6Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<GroupStatistic> GroupStatistic { get; set; }
        public virtual DbSet<GroupTime> GroupTime { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TimeLesson> TimeLesson { get; set; }
        public virtual DbSet<Timetable> Timetable { get; set; }
    }
}
