namespace CareerCloud.Pocos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco
    {
        [Key, Column(TypeName = "char(10)")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = true), MaxLength(50)]
        public string Name { get; set; }




        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
    }
}
