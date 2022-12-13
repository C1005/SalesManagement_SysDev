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
    class M_MajorCassification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [Column("McID", TypeName = "INT", Order = 0)]
        [DisplayName("大分類ID")]
        public int McID { get; set; }           //大分類ID

        [Required]
        [MaxLength(50)]
        [Column("McName", TypeName = "nvarchar", Order = 1)]
        [DisplayName("大分類名")]
        public String McName { get; set; }      //大分類名	
	
        [Required]
        [Column("McFlag", TypeName = "INT", Order = 2)]
        [DisplayName("大分類管理フラグ")]
        public int McFlag { get; set; }         //大分類管理フラグ

        [Column("McHidden", TypeName = "nvarchar", Order = 3)]
        [DisplayName("非表示理由")]
        public String McHidden { get; set; }	//非表示理由		

    }
}
