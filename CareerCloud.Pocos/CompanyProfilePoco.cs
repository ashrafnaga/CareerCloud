namespace CareerCloud.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Profiles")]
    public class CompanyProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Registration_Date", TypeName = "datetime2(7)")]
        public DateTime RegistrationDate { get; set; }

        [Column("Company_Website"), MaxLength(100)]
        public string CompanyWebsite { get; set; }

        [Column("Contact_Phone", TypeName = "varchar(20)"), Required(AllowEmptyStrings = true)]
        public string ContactPhone { get; set; }

        [Column("Contact_Name", TypeName = "varchar(50)")]
        public string ContactName { get; set; }

        [Column("Company_Logo", TypeName = "varbinary(max)")]
        public byte[] CompanyLogo { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }



        public virtual ICollection<CompanyJobPoco> CompanyJobs { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }
}
