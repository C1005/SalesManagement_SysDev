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
    class M_Maker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("MaID", TypeName = "INT", Order = 0)]
        [DisplayName("メーカID")]
        public int MaID { get; set; }           //メーカID

        [Required]
        [MaxLength(50)]
        [Column("MaName", TypeName = "nvarchar", Order = 1)]
        [DisplayName("メーカ名")]
        public String MaName { get; set; }      //メーカ名

        [Required]
        [MaxLength(50)]
        [Column("MaAdress", TypeName = "nvarchar", Order = 2)]
        [DisplayName("住所")]
        public String MaAdress { get; set; }    //住所

        [Required]
        [MaxLength(13)]
        [Column("MaPhone", TypeName = "nvarchar", Order = 3)]
        [DisplayName("電話番号")]
        public String MaPhone { get; set; }     //電話番号

        [Required]
        [MaxLength(7)]
        [Column("MaPostal", TypeName = "nvarchar", Order = 4)]
        [DisplayName("郵便番号")]
        public String MaPostal { get; set; }    //郵便番号

        [Required]
        [MaxLength(13)]
        [Column("MaFax", TypeName = "nvarchar", Order = 5)]
        [DisplayName("FAX")]
        public String MaFAX { get; set; }       //FAX

        [Required]
        [Column("MaFlag", TypeName = "INT", Order = 6)]
        [DisplayName("メーカ管理フラグ")]
        public int MaFlag { get; set; }         //メーカ管理フラグ

        [Column("MaHidden", TypeName = "nvarchar", Order = 7)]
        [DisplayName("非表示理由")]
        public String MaHidden { get; set; }	//非表示理由		
    }
}
