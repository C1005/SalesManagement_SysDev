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
    class M_Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ClID", TypeName = "INT", Order = 0)]
        [DisplayName("顧客ID")]
        public int ClID { get; set; }           //顧客ID

        [Required]
        [Column("SoID", TypeName = "INT", Order = 1)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }           //営業所ID

        [Required]
        [MaxLength(50)]
        [Column("ClName", TypeName = "nvarchar", Order = 2)]
        [DisplayName("顧客名")]
        public String ClName { get; set; }      //顧客名

        [Required]
        [MaxLength(50)]
        [Column("ClAddress", TypeName = "nvarchar", Order = 3)]
        [DisplayName("住所")]
        public String ClAddress { get; set; }   //住所	 

        [Required]
        [MaxLength(13)]
        [Column("ClPhone", TypeName = "nvarchar", Order = 4)]
        [DisplayName("電話番号")]
        public String ClPhone { get; set; }     //電話番号	

        [Required]
        [MaxLength(7)]
        [Column("ClPostal", TypeName = "nvarchar", Order = 5)]
        [DisplayName("郵便番号")]
        public String ClPostal { get; set; }       //郵便番号

        [Required]
        [MaxLength(13)]
        [Column("ClFAX", TypeName = "nvarchar", Order = 6)]
        [DisplayName("FAX")]
        public String ClFAX { get; set; }       //FAX		

        [Required]
        [Column("ClFlag", TypeName = "INT", Order = 7)]
        [DisplayName("顧客管理フラグ")]
        public int ClFlag { get; set; }         //顧客管理フラグ	

        [Column("ClHidden", TypeName = "nvarchar", Order = 8)]
        [DisplayName("非表示理由")]
        public String ClHidden { get; set; }	//非表示理由		

    }
}
