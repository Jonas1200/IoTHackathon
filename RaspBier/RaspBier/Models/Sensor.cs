using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaspBier.Models
{
    public class Sensor
    {

        [Key]
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class SensorValue
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Sensor Typ")]
        public SensorType SensorType { get; set; }

        [Required]
        [Display(Name = "Sensor ID")]
        public int SensorID { get; set; }

        [Required]
        [Display(Name = "Wert")]
        public int Value { get; set; }

        [Required]
        [Display(Name = "Zeitstempel")]
        public DateTime TimeStamp { get; set; }
    }

    public enum SensorType
    {
        [Display(Name = "Wassersensor")]
        WaterSensor = 0
    }


}
