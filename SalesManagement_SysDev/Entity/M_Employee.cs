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
    class M_Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("EmID", TypeName = "INT", Order = 0)]
        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID

        [Required]
        [MaxLength(50)]
        [Column("EmName", TypeName = "nvarchar", Order = 1)]
        [DisplayName("社員名")]
        public String EmName { get; set; }          //社員名	

        [Required]
        [Column("SoID", TypeName = "INT", Order = 2)]
        [DisplayName("営業所ID")]
        public int SoID { get; set; }               //営業所ID	

        [Required]
        [Column("PoID", TypeName = "INT", Order = 3)]
        [DisplayName("役職ID")]
        public int PoID { get; set; }               //役職ID

        [Required]
        [Column("EmHiredate", TypeName = "date", Order = 4)]
        [DisplayName("入社年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EmHiredate { get; set; }    //入社年月日

        [Required]
        [Column("EmPassword", TypeName = "nvarchar", Order = 5)]
        [DisplayName("パスワード")]
        public String EmPassword { get; set; }      //パスワード

        [Required]
        [MaxLength(13)]
        [Column("EmPhone", TypeName = "nvarchar", Order = 6)]
        [DisplayName("電話番号")]
        public String EmPhone { get; set; }         //電話番号	

        //[MaxLength(13)]
        //[Required]
        // public String EmBarcode { get; set; }    //社員バーコード	

        [Required]
        [Column("EmFlag", TypeName = "INT", Order = 7)]
        [DisplayName("社員管理フラグ")]
        public int EmFlag { get; set; }             //社員管理フラグ

        [Column("EmHidden", TypeName = "nvarchar", Order = 8)]
        [DisplayName("非表示理由")]
        public String EmHidden { get; set; }	    //非表示理由		
    }
}
