namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Locations")]
    public class CompanyLocationPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Company { get; set; }

        [Required(AllowEmptyStrings = true), Column("Country_Code", TypeName = "char(10)")]
        public string CountryCode { get; set; }

        [Column("State_Province_Code", TypeName = "char(10)")]
        public string Province { get; set; }

        [Column("Street_Address"), MaxLength(100)]
        public string Street { get; set; }

        [Column("City_Town"), MaxLength(100)]
        public string City { get; set; }

        [Column("Zip_Postal_Code", TypeName = "char(20)")]
        public string PostalCode { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }




        [ForeignKey("Company")]
        public virtual CompanyProfilePoco CompanyProfile { get; set; }

        [ForeignKey("CountryCode")]
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
    }
}
