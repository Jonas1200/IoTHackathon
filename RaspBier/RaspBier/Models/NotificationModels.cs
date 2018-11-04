using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaspBier.Models
{
    public class NotificationEntry
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
        [Display(Name = "Benachrichtigungstyp")]
        public NotificationType NotificationType { get; set; }

        [Required]
        [Display(Name = "Nachricht")]
        public String Message { get; set; }

        [Required]
        [Display(Name = "Zeitstempel")]
        public DateTime TimeStamp { get; set; }
    }

    public enum NotificationType
    {
        [Display(Name = "Benachrichtigung")]
        Notification = 0
    }
}
