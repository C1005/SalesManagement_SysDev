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
    class T_OperationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [Column("OpHistoryID", TypeName = "INT", Order = 0)]
        [DisplayName("ログイン操作ID")]
        public int OpHistoryID { get; set; }        //ログイン操作ID		

        [Required]
        [Column("EmID", TypeName = "INT", Order = 1)]
        [DisplayName("社員ID")]
        public int EmID { get; set; }               //社員ID

        [Required]
        [Column("ItemName", TypeName = "nvarchar", Order = 2)]
        [DisplayName("項目名")]
        public String ItemName { get; set; }        //項目名

        [Column("OpStartTime", TypeName = "date", Order = 3)]
        [DisplayName("操作開始時刻")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OpStartTime { get; set; }   //操作開始時刻	

        [Column("OpEndTime", TypeName = "date", Order = 4)]
        [DisplayName("操作終了時刻")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpEndTime { get; set; }	//操作終了時刻		

    }
}
