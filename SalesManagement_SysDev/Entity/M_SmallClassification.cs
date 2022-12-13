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
    class M_SmallClassification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [Column("ScID", TypeName = "INT", Order = 0)]
        [DisplayName("小分類ID")]
        public int ScID { get; set; }           //小分類ID		

        [Required]
        [Column("McID", TypeName = "INT", Order = 1)]
        [DisplayName("大分類ID")]
        public int McID { get; set; }           //大分類ID

        [Required]
        [MaxLength(50)]
        [Column("ScName", TypeName = "nvarchar", Order = 2)]
        [DisplayName("小分類名")]
        public String ScName { get; set; }      //小分類名		

        [Required]
        [Column("ScFlag", TypeName = "INT", Order = 3)]
        [DisplayName("小分類管理フラグ")]
        public int ScFlag { get; set; }         //小分類管理フラグ

        [Column("ScHidden", TypeName = "nvarchar", Order = 4)]
        [DisplayName("非表示理由")]
        public String ScHidden { get; set; }	//非表示理由		

    }
}
