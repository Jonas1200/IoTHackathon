using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaspBier.Models
{
    public class Error
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Sensor ID")]
        public int SensorID { get; set; }

        [Required]
        [Display(Name = "Error Typ")]
        public ErrorType ErrorType { get; set; }

        [Required]
        [Display(Name = "Nachricht")]
        public String Message { get; set; }

        [Required]
        [Display(Name = "Zeitstempel")]
        public DateTime TimeStamp { get; set; }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public enum ErrorType
    {
        [Display(Name = "Fehler")]
        Failure = 0,
        [Display(Name = "Warning")]
        Warning = 1,
        [Display(Name = "Wartung")]
        Maintenance = 2,

    }
}
