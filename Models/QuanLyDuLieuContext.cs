using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Module2._1.Models;

public partial class QuanLyDuLieuContext : DbContext
{
    public QuanLyDuLieuContext()
    {
    }

    public QuanLyDuLieuContext(DbContextOptions<QuanLyDuLieuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=connectString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
