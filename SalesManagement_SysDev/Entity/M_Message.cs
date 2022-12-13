using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev
{
    class M_Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(6)]
        [Column("MsgCD", TypeName = "char", Order = 0)]
        [DisplayName("メッセージ番号")]
        public string MsgCD { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("MsgComments", TypeName = "nvarchar", Order = 1)]
        [DisplayName("メッセージ内容")]
        public string MsgComments { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("MsgTitle", TypeName = "nvarchar", Order = 2)]
        [DisplayName("メッセージタイトル")]
        public string MsgTitle { get; set; }

        [Required]
        [Column("MsgButton", TypeName = "int", Order = 3)]
        [DisplayName("メッセージボタン")]
        public int MsgButton { get; set; }

        [Required]
        [Column("MsgICon", TypeName = "int", Order = 4)]
        [DisplayName("メッセージアイコン")]
        public int MsgICon { get; set; }
    }
}
