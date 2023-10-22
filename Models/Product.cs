using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Module2._1.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public string ProductId { get; set; } = null!;

    [Column("product_name")]
    [StringLength(450)]
    public string ProductName { get; set; } = null!;

    [StringLength(450)]
    public string? UrlImage { get; set; }

    [Column("price", TypeName = "decimal(20, 2)")]
    public long Price { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }
}
