using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class TeamSetting
    {
        [Key]
        [ForeignKey("Team")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamId { get; set; }
        public int DisplayMode { get; set; }
        public int PrimaryScreenId { get; set; }
        public int SecondaryScreenId { get; set; }
        public DateTime? AutoScreenStartTime { get; set; }
        public DateTime SecondaryScreenEndTime { get; set; }

        //public string CustomMessage { get; set; }


        //Auto Time
        public int FirstScreenTime { get; set; }
        public int SecondScreenTime { get; set; }
        //public int ThirdScreenTime { get; set; }
        public int FourthScreenTime { get; set; }
        public int FifthScreenTime { get; set; }
        public int SixthScreenTime { get; set; }
        public int SeventhScreenTime { get; set; }
        public int EighthScreenTime { get; set; }
        public int NinthScreenTime { get; set; }

        //ScreenMessage
        //public string FirstMessage { get; set; }
        //public string SecondMessage { get; set; }
        public string ThirdMessage { get; set; }
        public string FourthMessage { get; set; }
        public string FifthMessage { get; set; }
        public string SixthMessage { get; set; }
        public string SeventhMessage { get; set; }
        public string EighthMessage { get; set; }

        //Color
        public string ThirdColor { get; set; }
        public string FourthColor { get; set; }
        public string FifthColor { get; set; }
        public string SixthColor { get; set; }
        public string SeventhColor { get; set; }
        public string EighthColor { get; set; }

        //Default
        public string DefaultUpMessage { get; set; }
        public string DefaultDownMessage { get; set; }

        public virtual Team Team { get; set; }
    
    }
    
}
