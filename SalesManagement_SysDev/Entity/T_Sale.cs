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
    class T_Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("SaID", TypeName = "INT", Order = 0)]
        [DisplayName("売上ID")]
        public int SaID { get; set; }           //売上ID	

        [Required]
        [Column("ClID", TypeName = "INT", Order = 1)]
        [DisplayName("顧客ID")]
        public int ClID { get; set; }           //顧客ID	

        [Required]
        [Column("SoID", TypeName = "INT", Order = 2)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }           //営業所ID	

        [Required]
        [Column("EmID", TypeName = "INT", Order = 3)]
        [DisplayName("受注社員ID")]
        public int EmID { get; set; }           //受注社員ID	

        [Required]
        [Column("ChID", TypeName = "INT", Order = 4)]
        [DisplayName("受注ID")]
        public int ChID { get; set; }           //受注ID

        [Required]
        [Column("SaDate", TypeName = "date", Order = 5)]
        [DisplayName("売上日時")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime SaDate { get; set; }    //売上日時

        [Column("SaHidden", TypeName = "nvarchar", Order = 6)]
        [DisplayName("非表示理由")]
        public String SaHidden { get; set; }    //非表示理由

        [Required]
        [Column("SaFlag", TypeName = "INT", Order = 7)]
        [DisplayName("売上管理フラグ")]
        public int SaFlag { get; set; }	        //売上管理フラグ
    }

    class T_SaleDsp
    {
        [DisplayName("売上ID")]
        public int SaID { get; set; }           //売上ID	

        [DisplayName("顧客ID")]
        public int ClID { get; set; }           //顧客ID	

        [DisplayName("営業所ID")]
        public int SoID { get; set; }           //営業所ID	

        [DisplayName("受注社員ID")]
        public int EmID { get; set; }           //受注社員ID	

        [DisplayName("受注ID")]
        public int ChID { get; set; }           //受注ID

        [DisplayName("売上日時")]
        public DateTime SaDate { get; set; }    //売上日時

        [DisplayName("非表示理由")]
        public String SaHidden { get; set; }    //非表示理由

        [DisplayName("売上管理フラグ")]
        public int SaFlag { get; set; }	        //売上管理フラグ

        [DisplayName("売上明細ID")]
        public int SaDetailID { get; set; }         //売上明細ID

        [DisplayName("商品ID")]
        public int PrID { get; set; }               //商品ID

        [DisplayName("個数")]
        public int SaQuantity { get; set; }         //個数

        [DisplayName("合計金額")]
        public int SaPrTotalPrice { get; set; }	    //合計金額
    }
}
