using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models;

[Table("detalleordenes")]
[Index("MenuId", Name = "fk_detalle_menus")]
[Index("OrdenId", Name = "fk_detalle_ordenes")]
public partial class Detalleordene
{
    [Key]
    [Column("detalle_id", TypeName = "int(11)")]
    public int DetalleId { get; set; }

    [Column("orden_id", TypeName = "int(11)")]
    public int OrdenId { get; set; }

    [Column("menu_id", TypeName = "int(11)")]
    public int MenuId { get; set; }

    [Column("cantidad", TypeName = "int(11)")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    [Precision(10)]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey("MenuId")]
    [InverseProperty("Detalleordenes")]
    public virtual Menu Menu { get; set; } = null!;

    [ForeignKey("OrdenId")]
    [InverseProperty("Detalleordenes")]
    public virtual Ordene Orden { get; set; } = null!;
}
