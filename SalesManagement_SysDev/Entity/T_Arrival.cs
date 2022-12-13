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
    class T_Arrival
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ArID", TypeName = "INT", Order = 0)]
        [DisplayName("入荷ID")]
        public int ArID { get; set; }               //入荷ID	

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

        [Column("ArDate", TypeName = "date", Order = 5)]
        [DisplayName("入荷年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ArDate { get; set; }       //入荷年月日

        //NULL許容になってるが、実際にNULLが入るとエラーが出るので、0か1が必ず入る
        [Column("ArStateFlag", TypeName = "INT", Order = 6)]
        [DisplayName("入荷状態フラグ")]
        public int? ArStateFlag { get; set; }   //入荷状態フラグ

        [Required]
        [Column("ArFlag", TypeName = "INT", Order = 7)]
        [DisplayName("入荷管理フラグ")]
        public int ArFlag { get; set; }         //入荷管理フラグ

        [Column("ArHidden", TypeName = "nvarchar", Order = 8)]
        [DisplayName("非表示理由")]
        public String ArHidden { get; set; }	    //非表示理由
    }

    class T_ArrivalDsp
    {
        [DisplayName("入荷ID")]
        public int ArID { get; set; }               //入荷ID	

        [DisplayName("営業所ID")]
        public int SoID { get; set; }               //営業所ID	

        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID	

        [DisplayName("顧客ID")]
        public int ClID { get; set; }               //顧客ID	

        [DisplayName("受注ID")]
        public int OrID { get; set; }               //受注ID	

        [DisplayName("入荷年月日")]
        public DateTime? ArDate { get; set; }       //入荷年月日

        [DisplayName("入荷状態フラグ")]
        public int? ArStateFlag { get; set; }   //入荷状態フラグ

        [DisplayName("入荷管理フラグ")]
        public int ArFlag { get; set; }         //入荷管理フラグ

        [DisplayName("非表示理由")]
        public String ArHidden { get; set; }	    //非表示理由

        [DisplayName("入荷詳細ID")]
        public int ArDetailID { get; set; }     //入荷詳細ID

        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID

        [DisplayName("数量")]
        public int ArQuantity { get; set; }	    //数量
    }
}
