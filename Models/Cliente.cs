using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models;

[Table("clientes")]
[Index("Email", Name = "email", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("cliente_id", TypeName = "int(11)")]
    public int ClienteId { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("telefono")]
    [StringLength(30)]
    public string? Telefono { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
