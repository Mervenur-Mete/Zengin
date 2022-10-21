namespace Zng.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hizmetler")]
    public partial class Hizmetler
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Baslik { get; set; }

        [StringLength(500)]
        public string Icerik { get; set; }

        public string ImageName { get; set; }

        public DateTime? Tarih { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Keyword { get; set; }
    }
}
