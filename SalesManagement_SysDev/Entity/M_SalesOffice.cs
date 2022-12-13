using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev
{
    class M_SalesOffice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("SoID", TypeName = "INT", Order = 0)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }           //営業所ID

        [Required]
        [MaxLength(50)]
        [Column("SoName", TypeName = "nvarchar", Order = 1)]
        [DisplayName("営業所名")]
        public String SoName { get; set; }      //営業所名	

        [Required]
        [MaxLength(50)]
        [Column("SoAddress", TypeName = "nvarchar", Order = 2)]
        [DisplayName("住所")]
        public String SoAddress { get; set; }   //住所

        [Required]
        [MaxLength(13)]
        [Column("SoPhone", TypeName = "nvarchar", Order = 3)]
        [DisplayName("電話番号")]
        public String SoPhone { get; set; }     //電話番号

        [Required]
        [MaxLength(7)]
        [Column("SoPostal", TypeName = "nvarchar", Order = 4)]
        [DisplayName("郵便番号")]
        public String SoPostal { get; set; }    //郵便番号	

        [Required]
        [MaxLength(13)]
        [Column("SoFAXl", TypeName = "nvarchar", Order = 5)]
        [DisplayName("FAX")]
        public String SoFAX { get; set; }       //FAX

        [Required]
        [Column("SoFlag", TypeName = "INT", Order = 6)]
        [DisplayName("営業所管理フラグ")]
        public int SoFlag { get; set; }         //営業所管理フラグ

        [Column("SoHidden", TypeName = "nvarchar", Order = 7)]
        [DisplayName("非表示理由")]
        public String SoHidden { get; set; }	//非表示理由		
    }
}
