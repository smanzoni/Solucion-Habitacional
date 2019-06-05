﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Barrio")]
    public class Barrio
    {
        #region Props
       
        [Key]
        public int id_barrio { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string nombre_barrio { get; set; }

        [Required]
        public string descripcion { get; set; }
       
        public List<Vivienda> viviendas { get; set; }

        #endregion


        #region Metodos
        
        public bool esValido()
        {
            return 
                Utilidades.esCampoValido(this.nombre_barrio, 1, 255) &&
                Utilidades.esCampoValido(this.nombre_barrio, 1, 255);
        }

        #endregion

    }
}
