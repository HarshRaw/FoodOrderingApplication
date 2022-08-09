using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace ProjectApplication.Models
{
    [Table(name: "Tables")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Table ID")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Column(TypeName = "varchar(30)")]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }


        [Required(ErrorMessage = "{0} cannot be empty!")]
        [Display(Name = "Seats")]
        public int NumberOfSeats { get; set; }


        [Required]
        [DefaultValue(false)]
        [Display(Name = "Reserved")]
        public bool Reserved { get; set; }

        #region
        [Display(Name = "Customer")]

        public int  CustomerId{ get; set; }
        [ForeignKey(nameof(Table.CustomerId))]
        public Customer Customers { get; set; }
        #endregion


        #region Navigate Collection to Order
        public ICollection<Order> Orders { get; set; }
        #endregion

    }
}
