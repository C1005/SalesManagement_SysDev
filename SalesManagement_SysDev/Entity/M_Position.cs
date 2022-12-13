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
    class M_Position
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [Column("PoID", TypeName = "INT", Order = 0)]
        [DisplayName("役職ID")]
        public int PoID { get; set; }           //役職ID

        [Required]
        [MaxLength(50)]
        [Column("PoName", TypeName = "nvarchar", Order = 1)]
        [DisplayName("役職名")]
        public String PoName { get; set; }      //役職名	

        [Required]
        [Column("PoFlag", TypeName = "INT", Order = 2)]
        [DisplayName("役職管理フラグ")]
        public int PoFlag { get; set; }         //役職管理フラグ

        [Column("PoHidden", TypeName = "nvarchar", Order = 3)]
        [DisplayName("非表示理由")]
        public String PoHidden { get; set; }    //非表示理由		
    }
}
