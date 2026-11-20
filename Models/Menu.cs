using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models;

[Table("menus")]
public partial class Menu
{
    [Key]
    [Column("menu_id", TypeName = "int(11)")]
    public int MenuId { get; set; }

    [Column("nombre")]
    [StringLength(120)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column("precio")]
    [Precision(10)]
    public decimal Precio { get; set; }

    [Required]
    [Column("disponible")]
    public bool? Disponible { get; set; }

    [InverseProperty("Menu")]
    public virtual ICollection<Detalleordene> Detalleordenes { get; set; } = new List<Detalleordene>();
}
