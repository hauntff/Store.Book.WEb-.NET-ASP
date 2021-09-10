using Store.Book.Model;
using Store.Book.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Book.WEb.Extensions
{
    public static class ModelExtensions
    {
        public static AuthorDto Transform(this Author p)
        {
            return new AuthorDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                SurName = p.SurName,
                Title = p.Title
            };
        }
    }
}