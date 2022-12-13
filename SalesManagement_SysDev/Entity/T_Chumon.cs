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
    class T_Chumon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ChID", TypeName = "INT", Order = 0)]
        [DisplayName("注文ID")]
        public int ChID { get; set; }               //注文ID	

        [Required]
        [Column("SoID", TypeName = "INT", Order = 1)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }               //営業所ID	

        //仕様書ではNULL許容になってるが、SQLServerではNOT NULLになっている
        [Column("EmID", TypeName = "INT", Order = 2)]
        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID	

        [Required]
        [Column("ClID", TypeName = "INT", Order = 3)]
        [DisplayName("顧客ID")]
        public int ClID { get; set; }               //顧客ID	

        [Required]
        [Column("OrID", TypeName = "INT", Order = 4)]
        [DisplayName("受注ID")]
        public int OrID { get; set; }               //受注ID

        [Required]
        [Column("ChDate", TypeName = "date", Order = 5)]
        [DisplayName("注文年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ChDate { get; set; }       //注文年月日

        //NULL許容になってるが、実際にNULLが入るとエラーが出るので、0か1が必ず入る
        [Required]
        [Column("ChStateFlag", TypeName = "INT", Order = 6)]
        [DisplayName("注文状態フラグ")]
        public int? ChStateFlag { get; set; }    //注文状態フラグ

        [Required]
        [Column("ChFlag", TypeName = "INT", Order = 7)]
        [DisplayName("注文管理フラグ")]
        public int ChFlag { get; set; }          //注文管理フラグ

        [Column("ChHidden", TypeName = "nvarchar", Order = 8)]
        [DisplayName("非表示理由")]
        public String ChHidden { get; set; }	    //非表示理由
    }

    class T_ChumonDsp
    {
        [DisplayName("注文ID")]
        public int ChID { get; set; }               //注文ID	

        [DisplayName("営業所ID")]
        public int SoID { get; set; }               //営業所ID	

        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID	

        [DisplayName("顧客ID")]
        public int ClID { get; set; }               //顧客ID	

        [DisplayName("受注ID")]
        public int OrID { get; set; }               //受注ID

        [DisplayName("注文年月日")]
        public DateTime? ChDate { get; set; }       //注文年月日

        [DisplayName("注文状態フラグ")]
        public int? ChStateFlag { get; set; }    //注文状態フラグ

        [DisplayName("注文管理フラグ")]
        public int ChFlag { get; set; }          //注文管理フラグ

        [DisplayName("非表示理由")]
        public String ChHidden { get; set; }	    //非表示理由

        [DisplayName("注文詳細ID")]
        public int ChDetailID { get; set; }     //注文詳細ID

        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [DisplayName("数量")]
        public int ChQuantity { get; set; }	    //数量
    }
}
