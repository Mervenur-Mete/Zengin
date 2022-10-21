namespace Zng.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Referanslar")]
    public partial class Referanslar
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Baslik { get; set; }

        [StringLength(500)]
        public string Icerik { get; set; }

        public string ImageName { get; set; }

        [StringLength(500)]
        public string Tarih { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Keyword { get; set; }
    }
}
