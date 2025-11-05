using System.ComponentModel.DataAnnotations;

namespace New_Game_MVC.Models
{
    
        public class GameResult
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(10)]
            public Choice PlayerChoice { get; set; }

            [Required]
            [StringLength(10)]
            public Choice ComputerChoice { get; set; }

            [Required]
            [StringLength(10)]
            public string Result { get; set; }

            public DateTime GameDate { get; set; } = DateTime.Now;
        }

    }

