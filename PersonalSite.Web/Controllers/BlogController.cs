using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FubuMVC.Core.Continuations;
using PersonalSite.Core.Contracts;
using PersonalSite.Core.Entities;

namespace PersonalSite.Web.Controllers
{
    public class BlogController
    {
        private readonly IPostRepository postRepository;

        public BlogController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public PostsListModel List()
        {
            return new PostsListModel()
                       {
                           Posts = postRepository.GetAll().ToList()
                       };
        }

        public CreatePostOutputModel Create()
        {
            return new CreatePostOutputModel();
        }

        public FubuContinuation Create(CreatePostOutputModel model)
        {
            postRepository.AddNew(model.DomainObject);
            return FubuContinuation.RedirectTo<BlogController>(c => c.List());
        }
    }

    public class CreatePostOutputModel: ModelBase<Post>
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public abstract class ModelBase<T>
    {
        public T DomainObject
        {
            get
            {
                Mapper.CreateMap(GetType(), typeof (T));
                return Mapper.Map<T>(this);
            }
        }
    }

    public class PostsListModel
    {
        public List<Post> Posts { get; set; }
    }
}