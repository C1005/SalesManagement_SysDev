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
    class T_Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("ShID", TypeName = "INT", Order = 0)]
        [DisplayName("出荷ID")]
        public int ShID { get; set; }               //出荷ID		

        [Required]
        [Column("ClID", TypeName = "INT", Order = 1)]
        [DisplayName("顧客ID")]
        public int ClID { get; set; }               //顧客ID

        [Column("EmID", TypeName = "INT", Order = 2)]
        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID		

        [Required]
        [Column("SoID", TypeName = "INT", Order = 3)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }               //営業所ID	

        [Required]
        [Column("OrID", TypeName = "INT", Order = 4)]
        [DisplayName("受注ID")]
        public int OrID { get; set; }               //受注ID		
        
        [Column("ShStateFlag", TypeName = "INT", Order = 5)]
        [DisplayName("出荷状態フラグ")]
        public int? ShStateFlag { get; set; }	//出荷状態フラグ   

        [Column("ShFinishDate", TypeName = "date", Order = 6)]
        [DisplayName("出荷完了年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ShFinishDate { get; set; }  //出荷完了年月日  

        [Required]
        [Column("ShFlag", TypeName = "INT", Order = 7)]
        [DisplayName("出荷管理フラグ")]
        public int ShFlag { get; set; }     	//出荷管理フラグ   

        [Column("ShHidden", TypeName = "nvarchar", Order = 8)]
        [DisplayName("非表示理由")]
        public String ShHidden { get; set; }	    //非表示理由
    }

    class T_ShipmentDsp
    {
        [DisplayName("出荷ID")]
        public int ShID { get; set; }               //出荷ID		

        [DisplayName("顧客ID")]
        public int ClID { get; set; }               //顧客ID

        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID		

        [DisplayName("営業所ID")]
        public int SoID { get; set; }               //営業所ID	

        [DisplayName("受注ID")]
        public int OrID { get; set; }               //受注ID		

        [DisplayName("出荷状態フラグ")]
        public int? ShStateFlag { get; set; }	//出荷状態フラグ   

        [DisplayName("出荷完了年月日")]
        public DateTime? ShFinishDate { get; set; }  //出荷完了年月日  

        [DisplayName("出荷管理フラグ")]
        public int ShFlag { get; set; }       	//出荷管理フラグ   

        [DisplayName("非表示理由")]
        public String ShHidden { get; set; }	    //非表示理由

        [DisplayName("出荷詳細ID")]
        public int ShDetailID { get; set; }     //出荷詳細ID    

        [DisplayName("商品ID")]
        public int PrID { get; set; }           //商品ID  

        [DisplayName("数量")]
        public int ShDquantity { get; set; }	//数量
    }
}
