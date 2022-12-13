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
    class T_LoginHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [Column("LoHistoryID", TypeName = "INT", Order = 0)]
        [DisplayName("ログイン認証ID")]
        public int LoHistoryID { get; set; }        //ログイン認証ID

        [Required]
        [Column("EmID", TypeName = "INT", Order = 1)]
        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID

        [Column("LoginDate", TypeName = "date", Order = 2)]
        [DisplayName("ログイン年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime LoginDate { get; set; }     //ログイン年月日

        [Column("LogoutDate", TypeName = "date", Order = 3)]
        [DisplayName("ログアウト年月日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? LogoutDate { get; set; }	//ログアウト年月日

    }
}
