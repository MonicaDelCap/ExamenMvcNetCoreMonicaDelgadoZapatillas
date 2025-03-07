﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenMvcNetCoreMonicaDelgado.Models
{
    [Table("IMAGENESZAPASPRACTICA")]
    public class Imagen
    {
        [Key]
        [Column("IDIMAGEN")]
        public int IdImagen { get; set; }

        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }

        [Column("IMAGEN")]
        public string ImagenNombre { get; set; }
    }
}
