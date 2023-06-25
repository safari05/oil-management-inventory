using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oil.Management.Entities.ApplMgt
{
    [Table("tb_user")]
    public class TbUser
    {
        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }
        [Column("id_role")]
        public int IdRole { get; set; }

        [Column("username")]
        [MaxLength(32, ErrorMessage = "Username maksimal 32 karakter")]
        public string Username { get; set; }
        [Column("password")]
        [MaxLength(12, ErrorMessage = "Password maksimal 12 karakter")]
        public string Password { get; set; }
        [Column("email")]
        [MaxLength(64, ErrorMessage = "Email maksimal 64 karakter")]
        public string Email { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("middle_name")]
        public string MiddleName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("file_image")]
        public string FileImage { get; set; }


        [Column("status")]
        public bool Status { get; set; }
        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Column("code_forgot")]
        public string? CodeForgot { get; set; }

        [Column("expired_code")]
        public DateTime? ExpiredCode { get; set; }

        [Column("id_type_user")]
        public int? IdTypeUser { get; set; }
        
        [Column("id_subsidiary_company")]
        public int? IdSubsidiaryCompany { get; set; }

        [Column("id_customer")]
        public int? IdCustomer { get; set; }

    }
}
