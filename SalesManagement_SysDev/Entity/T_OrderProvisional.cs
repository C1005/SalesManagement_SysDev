using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev.Entity
{
    class T_OrderProvisional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [Column("OrID", TypeName = "INT", Order = 0)]
        [DisplayName("受注ID")]
        public int OrID { get; set; }           //受注ID	

        [Required]
        [Column("SoID", TypeName = "INT", Order = 1)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }           //営業所ID		

        [Required]
        [Column("EmID", TypeName = "INT", Order = 2)]
        [DisplayName("社員ID")]
        public int EmID { get; set; }           //社員ID		

        [Required]
        [Column("ClID", TypeName = "INT", Order = 3)]
        [DisplayName("顧客ID")]
        public int ClID { get; set; }           //顧客ID

        [Required]
        [MaxLength(50)]
        [Column("ClCharge", TypeName = "nvarchar", Order = 4)]
        [DisplayName("顧客担当者名")]
        public String ClCharge { get; set; }    //顧客担当者名

        [Required]
        [Column("OrDate", TypeName = "date", Order = 5)]
        [DisplayName("受注年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OrDate { get; set; }    //受注年月日

        [Column("OrStateFlag", TypeName = "INT", Order = 6)]
        [DisplayName("受注状態フラグ")]
        public int? OrStateFlag { get; set; }    //受注状態フラグ


        [Required]
        [Column("OrFlag", TypeName = "INT", Order = 7)]
        [DisplayName("受注管理フラグ")]
        public int OrFlag { get; set; } //受注管理フラグ

        [Column("OrHidden", TypeName = "nvarchar", Order = 8)]
        [DisplayName("非表示理由")]
        public String OrHidden { get; set; }    //非表示理由
    }

    class T_OrderProvisionalDsp
    {
        [DisplayName("受注ID")]
        public int OrID { get; set; }           //受注ID	

        [DisplayName("営業所ID")]
        public int SoID { get; set; }           //営業所ID		

        [DisplayName("社員ID")]
        public int EmID { get; set; }           //社員ID	

        [DisplayName("顧客ID")]
        public int ClID { get; set; }           //顧客ID

        [DisplayName("顧客担当者名")]
        public String ClCharge { get; set; }    //顧客担当者名

        [DisplayName("受注年月日")]
        public DateTime OrDate { get; set; }    //受注年月日

        [DisplayName("受注状態フラグ")]
        public int? OrStateFlag { get; set; }    //受注状態フラグ

        [DisplayName("受注管理フラグ")]
        public int OrFlag { get; set; }         //受注管理フラグ

        [DisplayName("非表示理由")]
        public String OrHidden { get; set; }    //非表示理由

        [DisplayName("受注詳細ID")]
        public int OrDetailID { get; set; }     //受注詳細ID

        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [DisplayName("数量")]
        public int OrQuantity { get; set; }	    //数量

        [DisplayName("合計金額")]
        public int OrTotalPrice { get; set; }	//合計金額
    }
}
