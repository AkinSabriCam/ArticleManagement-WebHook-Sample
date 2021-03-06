using System;
using System.Collections.Generic;
using Common.Entity;
using Domain.Article.Events;
using FluentValidation;

namespace Domain.Article
{
    public class Article : AggregateRoot<Guid>
    {
        public string Code { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public ArticleCategory Category { get; set; }
        public string AuthorName { get; private set; }
        public string AuthorLastName { get; private set; }


        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ValidationException("Code can not be empty!");

            Code = code;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ValidationException("Title can not be empty!");

            Title = title;
        }

        public void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ValidationException("Content can not be empty!");

            Content = content;
        }

        public void SetAuthor(string name, string lastName)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName))
                throw new ValidationException("Author name or lastname can not be empty!");
            AuthorName = name;
            AuthorLastName = lastName;
        }

        public void Created(List<string> articleCodes)
        {
            AddDomainEvent(new CreatedArticleEvent(this, articleCodes));
        }
    }
}