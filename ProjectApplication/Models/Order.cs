using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace ProjectApplication.Models
{
    [Table(name: "Orders")]

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Order ID")]
        public int Id { get; set; }


        //#region Customer Link

        //[Display(Name =" Customer ")]
        //public int CustomerId { get; set; }
        //[ForeignKey(nameof(Customer.CustomerId))]
        //public Customer Customers { get; set; }

        //#endregion




        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }



        #region TableLink
        [Display(Name ="Table Name")]
        public int TableId { get; set; }
        [ForeignKey(nameof(Order.TableId))]
        public Table Tables { get; set; }

        #endregion



        #region Dish Link 

        [Display(Name ="Dish")]
        public int DishId { get; set; }
        [ForeignKey(nameof(Order.DishId))]
        public Dish Dishes { get; set; }

        #endregion


        [Required(ErrorMessage = "Don't leave {0} Empty!")]
        [Display(Name = "Quantity(Number of Items)")]
        [DefaultValue(1)]
        public short Quantity { get; set; }



        #region Payment Link

        [Display(Name ="Payment Method")]
        public int PaymentId { get; set; }
        [ForeignKey(nameof(Order.PaymentId))]
        public PaymentMethod PaymentMethods { get; set; }

        #endregion


        [Required]
        [DataType(DataType.Currency)]
        [Display(Name ="Price")]
        public float Price { get; set; }

        [Required]
        [DefaultValue(true)]
        [Display(Name = "Order Placed")]
        public bool OrderPlaced { get; set; }


    }
}
