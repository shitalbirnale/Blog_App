using BlogApplication.Constants;
using BlogApplication.Data;
using BlogApplication.IBlogServices;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
namespace BlogApplication.BlogService_Implemetation
{
    [ExcludeFromCodeCoverage]
    public class BlogServiceImplementation : IBlog
    {
        private readonly ApplicationDbContext context;
        public BlogServiceImplementation(ApplicationDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// BlogPost API fetch all blog post that have been created
        /// </summary>
        /// <returns></returns>
        public List<Blog> BlogList()
        {
            List<Blog> blogDetailsList = new List<Blog>();
            try
            {
                var  blogList = context.BlogDetails.Where(x => x.Isdeleted == false).
                                 OrderByDescending(b => b.Id).AsNoTracking().ToList();
                if (blogList is not null && blogList.Any())
                {
                    foreach (var i in blogList)
                    {
                        Blog blogDetail = new Blog();
                        blogDetail.Id = i.Id;
                        blogDetail.Title = i.Title;
                        blogDetail.AutherName = i.AutherName;
                        System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                        blogDetail.Contents = rx.Replace(i.Contents, "");
                        blogDetail.PublicationDate = i.PublicationDate;
                        blogDetailsList.Add(blogDetail);
                    }
                    return blogDetailsList;
                }
                else 
                {
                    return blogDetailsList;
                }    
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreateBlog service add new blog post with blog related details
        /// </summary>
        /// <param name="BlogM"></param>
        /// <returns></returns>
        public string  CreateBlog(Blog BlogM)
        {
            string isSuccess = string.Empty;
            try
            {
                Blog blog = new Blog();
                blog.AutherName = BlogM.AutherName;
                blog.Title = BlogM.Title;
                blog.Contents = BlogM.Contents;
                blog.PublicationDate = BlogM.PublicationDate;
                blog.CreatedDate = DateTime.Now;
                blog.CreatedBy = "";
                context.BlogDetails.Add(blog);
                context.SaveChanges();
                isSuccess = BlogConstant.SUCCESSMESSAGE;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }

        /// <summary>
        /// Edit blogId
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public Blog Edit(int blogId)
        {
            Blog Model = new Blog();
            if (blogId > 0)
            {
                var blogDetails = context.BlogDetails.Where(x => x.Id == blogId).FirstOrDefault();
                Model.Id = blogDetails.Id;
                Model.AutherName= blogDetails.AutherName;
                Model.Title = blogDetails.Title;
                Model.Contents = blogDetails.Contents;
                Model.PublicationDate= blogDetails.PublicationDate; 
            }
            return Model;
        }

        /// <summary>
        /// Edit Service update the specific blog post with updated data
        /// </summary>
        /// <param name="BlogM"></param>
        /// <returns></returns>
        public string Edit(Blog BlogM)
        {
            string isSuccessMessage = "";
            var blogdetail = context.BlogDetails.Where(x => x.Id == BlogM.Id).FirstOrDefault();
            try
            {
                if (blogdetail is not null)
                {
                    blogdetail.Id = BlogM.Id;
                    blogdetail.AutherName = BlogM.AutherName;
                    blogdetail.Title = BlogM.Title;
                    blogdetail.Contents = BlogM.Contents;
                    blogdetail.PublicationDate = BlogM.PublicationDate;
                    blogdetail.UpdatedDate = DateTime.Now;
                    context.BlogDetails.Update(blogdetail); 
                    context.SaveChanges();
                    isSuccessMessage = BlogConstant.SUCCESSMESSAGE;
                }
                return isSuccessMessage;
            }
            catch(Exception ex) 
            {
                throw ex;           
            }   
        }

        /// <summary>
        /// Delete Service from blog list
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int blogId)
        {
            bool isDeleted = false;
            if(blogId != 0)
            {
                var blogDetails = context.BlogDetails.Where(x => x.Id == blogId).FirstOrDefault();
                if (blogDetails != null) 
                {
                    context.BlogDetails.Remove(blogDetails);
                    context.SaveChanges();
                    isDeleted = true;
                    return isDeleted;
                }
            }
            return isDeleted;
        }
    }
}
