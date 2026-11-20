using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models;

[Table("ordenes")]
[Index("ClienteId", Name = "fk_ordenes_clientes")]
public partial class Ordene
{
    [Key]
    [Column("orden_id", TypeName = "int(11)")]
    public int OrdenId { get; set; }

    [Column("cliente_id", TypeName = "int(11)")]
    public int ClienteId { get; set; }

    [Column("fecha_orden", TypeName = "datetime")]
    public DateTime FechaOrden { get; set; }

    [Column("estado")]
    [StringLength(30)]
    public string Estado { get; set; } = null!;

    [Column("total")]
    [Precision(10)]
    public decimal Total { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Ordenes")]
    public virtual Cliente Cliente { get; set; } = null!;

    [InverseProperty("Orden")]
    public virtual ICollection<Detalleordene> Detalleordenes { get; set; } = new List<Detalleordene>();
}
