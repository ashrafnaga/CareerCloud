namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Applicant_Skills")]
    public class ApplicantSkillPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Applicant { get; set; }

        [Required(AllowEmptyStrings = true), MaxLength(100)]
        public string Skill { get; set; }

        [Column("Skill_Level", TypeName = "char(10)"), Required(AllowEmptyStrings = true)]
        public string SkillLevel { get; set; }

        [Column("Start_Month")]
        public byte StartMonth { get; set; }

        [Column("Start_Year")]
        public int StartYear { get; set; }

        [Column("End_Month")]
        public byte EndMonth { get; set; }

        [Column("End_Year")]
        public int EndYear { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }



        [ForeignKey("Applicant")]
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
