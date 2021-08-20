namespace CareerCloud.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Company { get; set; }

        [Column("Profile_Created", TypeName = "datetime2(7)"), DefaultValue("getdate()")]
        public DateTime ProfileCreated { get; set; }

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }

        [Column("Is_Company_Hidden"), DefaultValue(0)]
        public bool IsCompanyHidden { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }




        [ForeignKey("Company")]
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { set; get; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills { set; get; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations { set; get; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescriptions { set; get; }
    }
}
