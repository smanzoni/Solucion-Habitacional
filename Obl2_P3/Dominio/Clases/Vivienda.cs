﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Vivienda")]
    public abstract class Vivienda
    {
        #region Props

        [Key]
        public int id_vivienda { get; set; }

        [Required]
        public string estado { get; set; }

        [Required]
        public string calle { get; set; }

        [Required]
        public int nro_puerta { get; set; }

        [Required]
        public string descripcion { get; set; }

        public Barrio barrio { get; set; }

        [Required]
        public int banios { get; set; }

        [Required]
        public int dormitorios { get; set; }

        [Required]
        public decimal metraje { get; set; }

        [Required]
        public int anio_construccion { get; set; }

        public Parametro moneda { get; set; }

        [Required]
        public decimal precio_final { get; set; }

        public Sorteo sorteo { get; set; }

        #endregion

        #region Metodos

        public virtual bool esValida()
        {
            return 
                (estado == "Recibida" || estado == "Habilitada" || estado == "Inhabilitada") &&
                Utilidades.esCampoValido(this.calle) &&
                this.nro_puerta > 0 && this.nro_puerta < 9999 &&
                Utilidades.esCampoValido(this.descripcion) &&
                this.banios > 0 &&
                this.dormitorios > 0 &&
                this.precio_final > 0 &&
                this.metraje > 0;
        }
        
        #endregion
    }
}
