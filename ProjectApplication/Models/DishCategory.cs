using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace ProjectApplication.Models
{
    [Table(name: "DishCategories")]

    public class DishCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "DishCategoriesID")]  //Dish Category ID
        public int DcId { get; set; }


        [Required(ErrorMessage = "Don't leave {0} Empty!")]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Dish Categories")]
        public string Categories { get; set; }


        #region Navigate Collection to Dish
        public ICollection<Dish> Dishes { get; set; }
        #endregion

    }
}
