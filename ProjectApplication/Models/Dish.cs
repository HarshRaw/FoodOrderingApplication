using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace ProjectApplication.Models
{
    [Table(name: "Dishes")]

    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="DishID")]
        public int DishId { get; set; }


        #region Directing to Dish Category


        [Display(Name ="Dish Category")]
        public int DishCategoryID { get; set; }
        [ForeignKey(nameof(Dish.DishCategoryID))]
        public DishCategory DishCategory { get; set; }

        #endregion



        [Required(ErrorMessage = "Don't leave {0} Empty!")]
        [Column(TypeName = "varchar(100)")]
        [Display(Name ="Dish")]
        public string DishName { get; set; }


        [Required]
        [DefaultValue(true)]
        [Display(Name = "Available")]
        public bool Available { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public float Price { get; set; }


        [Display(Name = "Food Image")]
        public string ImgUrl { get; set; } = null;

        #region Navigate Collection to Order
        public ICollection<Order> Orders { get; set; }
        #endregion

    }
}
