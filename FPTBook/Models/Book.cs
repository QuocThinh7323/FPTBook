using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Author")]
        public int AuthorID { get; set; }
        public Author? Author { get; set; }


        [Display(Name = "Publishing Company")]
        public int PublishingCompanyID { get; set; }
        public PublishingCompany? PublishingCompany { get; set; }


        public int Quantity { get; set; }
        public string? Description { get; set; }



        public string? ImgFileName { get; set; } // Thêm thuộc tính kiểu string để lưu trữ tên tệp hình ảnh

        public string? ImgFileExt { get; set; }
        // Thêm thuộc tính kiểu string để lưu trữ tên tệp hình ảnh

        public string FullPath { get { return $@"~/Images/{ImgFileName}"; } }
        // Thêm thuộc tính FullPath để lưu trữ đường dẫn đến hình ảnh sản phẩm

    }

    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? Books { get; set; }
        public Category() { }
        public Category(TmpCategory tc)
        {
            this.Name = tc.Name;
        }
    }

    public class PublishingCompany
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}

