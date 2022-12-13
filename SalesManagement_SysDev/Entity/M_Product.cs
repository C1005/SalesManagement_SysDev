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
    class M_Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        [Column("PrID", TypeName = "INT", Order = 0)]
        [DisplayName("商品ID")]
        public int PrID { get; set; }               //商品ID		

        [Required]
        [Column("MaID", TypeName = "INT", Order = 1)]
        [DisplayName("メーカID")]
        public int MaID { get; set; }               //メーカID	

        [Required]
        [MaxLength(50)]
        [Column("PrName", TypeName = "nvarchar", Order = 2)]
        [DisplayName("商品名")]
        public String PrName { get; set; }           //商品名		

        [Column("Price", TypeName = "INT", Order = 3)]
        [DisplayName("価格")]
        public int Price { get; set; }              //価格	
        
        [Required]
        [Column("PrSafetyStock", TypeName = "INT", Order = 4)]
        [DisplayName("安全在庫数")]
        public int PrSafetyStock { get; set; }      //安全在庫数		

        [Required]
        [Column("ScID", TypeName = "INT", Order = 5)]
        [DisplayName("小分類ID")]
        public int ScID { get; set; }               //小分類ID	                                                

        [Required]
        [MaxLength(20)]
        [Column("PrModelNumber", TypeName = "nvarchar", Order = 6)]
        [DisplayName("型番")]
        public String PrModelNumber { get; set; }      //型番

        [Required]
        [MaxLength(20)]
        [Column("PrColor", TypeName = "nvarchar", Order = 7)]
        [DisplayName("色")]
        public String PrColor { get; set; }         //色		

        [Required]
        [Column("PrReleaseDate", TypeName = "date", Order = 8)]
        [DisplayName("発売日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PrReleaseDate { get; set; } //発売日		

        [Required]
        [Column("PrFlag", TypeName = "INT", Order = 9)]
        [DisplayName("商品管理フラグ")]
        public int PrFlag { get; set; }             //商品管理フラグ

        [Column("PrHidden", TypeName = "nvarchar", Order = 10)]
        [DisplayName("非表示理由")]
        public String PrHidden { get; set; }	    //非表示理由		
    }
}
