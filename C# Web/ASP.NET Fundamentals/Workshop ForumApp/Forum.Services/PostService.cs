﻿using Microsoft.EntityFrameworkCore;
using Forum.Data;
using Forum.ViewModels.Post;
using Forum.Data.Models;

namespace Forum.Services
{
	public class PostService : IPostService
	{
		private readonly ForumDbContext dbContext;

		public PostService(ForumDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

        public async Task<IEnumerable<PostViewModel>> ListAllAsync()
		{
			IEnumerable<PostViewModel> allPosts = await dbContext
				.Posts
				.Select(p => new PostViewModel()
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					Content = p.Content
				})
				.ToArrayAsync();

			return allPosts;
		}

		
        public async Task AddPostAsync(PostFormModel postViewModel)
        {
            Post newPost = new Post()
            {
                Title = postViewModel.Title,
                Content = postViewModel.Content
            };

            await this.dbContext.Posts.AddAsync(newPost);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<PostFormModel> GetForEditOrDeleteByIdAsync(string id)
        {
            Post postToEdit = await this.dbContext
               .Posts
               .FirstAsync(p => p.Id.ToString() == id);

            return new PostFormModel()
            {
                Title = postToEdit.Title,
                Content = postToEdit.Content
            };
        }

        public async Task EditByIdAsync(string id, PostFormModel postEditedModel)
        {
            Post postToEdit = await this.dbContext
                .Posts
                .FirstAsync(p => p.Id.ToString() == id);

            postToEdit.Title = postEditedModel.Title;
            postToEdit.Content = postEditedModel.Content;

            await this.dbContext.SaveChangesAsync();
        }

		public async Task DeleteByIdAsync(string id)
		{
			Post postToDelete = await this.dbContext
				.Posts
				.FirstAsync(p => p.Id.ToString() == id);

			this.dbContext.Posts.Remove(postToDelete);
			await this.dbContext.SaveChangesAsync();
		}
	}
}
