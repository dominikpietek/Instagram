using Bogus;
using Instagram.Databases;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

namespace Instagram.GenerateFakeData
{
    public class CreateFakeData
    {
        private int _lastId;
        public CreateFakeData()
        {
            List<string> loremipsum = new List<string>();
            loremipsum.Add("Lorem ipsum dolor sit amet, consectetur adipiscing elit");
            loremipsum.Add("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco");
            loremipsum.Add("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore");
            loremipsum.Add("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            loremipsum.Add("Lorem ipsum dolor sit amet");
            Randomizer.Seed = new Random(Seed: 594);
            Random random = new Random();
            List<User> users = new List<User>();
            using (InstagramDbContext db = new InstagramDbContext("FakeDataDb"))
            {
                // truncate
                _lastId = db.Users.OrderBy(u => u.Id).Last().Id;
                db.Users.RemoveRange(db.Users);
                db.Posts.RemoveRange(db.Posts);
                db.Tags.RemoveRange(db.Tags);
                db.Comments.RemoveRange(db.Comments);
                db.CommentResponses.RemoveRange(db.CommentResponses);
                db.ProfileImages.RemoveRange(db.ProfileImages);
                db.PostImages.RemoveRange(db.PostImages);
                db.Stories.RemoveRange(db.Stories);
                db.StoryImages.RemoveRange(db.StoryImages);
                db.Friends.RemoveRange(db.Friends);
                db.GotFriendRequestModels.RemoveRange(db.GotFriendRequestModels);
                db.SentFriendRequestModels.RemoveRange(db.SentFriendRequestModels);
                db.Messages.RemoveRange(db.Messages);
                // set Users data
                for (int i = 0; i < 10; i++)
                {
                    users.Add(GenerateFakeUser());
                }
                for (int i = 0; i < users.Count; i++)
                {
                    db.Users.Add(users[i]);
                }
                db.SaveChanges();
                // create Friendships
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 6; j < 11; j++)
                    {
                        db.Friends.Add(new Friend()
                        {
                            UserId = i + _lastId,
                            FriendId = j + _lastId
                        });
                        db.Friends.Add(new Friend()
                        {
                            FriendId = i + _lastId,
                            UserId = j + _lastId
                        });
                        // messages
                        for (int k = 0; k < random.Next(35); k++)
                        {
                            db.Messages.Add(new Message()
                            {
                                UserId = i + _lastId,
                                FriendId = j + _lastId,
                                SendDate = new DateTime(random.Next(2020, 2024), random.Next(1, 12), random.Next(1, 28)),
                                Content = loremipsum[random.Next(5)]
                            });
                        }
                        for (int k = 0; k < random.Next(35); k++)
                        {
                            db.Messages.Add(new Message()
                            {
                                FriendId = i + _lastId,
                                UserId = j + _lastId,
                                SendDate = new DateTime(random.Next(2020, 2024), random.Next(1, 12), random.Next(1, 28)),
                                Content = loremipsum[random.Next(5)]
                            });
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (i != 5)
                    {
                        db.SentFriendRequestModels.Add(new SentFriendRequestModel()
                        {
                            UserId = 6 + _lastId,
                            StoredUserId = i + 1 + _lastId
                        });
                        db.GotFriendRequestModels.Add(new GotFriendRequestModel()
                        {
                            StoredUserId = 6 + _lastId,
                            UserId = i + 1 + _lastId
                        });
                    }
                }
                db.SaveChanges();
            }

            using (StreamWriter file = new StreamWriter("C:\\Programs\\Instagram\\Instagram\\GenerateFakeData\\loginData.txt"))
            {
                for (int i = 0; i < users.Count; i++)
                {
                    file.WriteLine($"{users[i].Nickname}    P4ssw0rd");
                }
            }
        }

        private User GenerateFakeUser()
        {
            Faker<User> fakeUser = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Nickname, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.EmailAdress, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Birthdate, f => new DateTime(f.Random.Int(1950, 2024), f.Random.Int(1, 12), f.Random.Int(1, 28)))
                .RuleFor(u => u.ProfilePhoto, f => new ProfileImage()
                {
                    ImageBytes = ConvertImage.ToByteArray($"C:\\Programs\\Instagram\\Instagram\\GenerateFakeData\\FakeProfilePhotos\\{f.Random.Int(1, 10)}.jpg")
                })
                .RuleFor(u => u.Password, f => Hash.HashString("P4ssw0rd"))
                .RuleFor(u => u.Posts, f => GenerateFakePosts())
                .RuleFor(u => u.Stories, f => GenerateFakeStories());
            return fakeUser.Generate();
        }

        private Post GenerateFakePost()
        {
            Faker<Post> fakePost = new Faker<Post>()
                .RuleFor(u => u.Description, f => f.Lorem.Paragraph(3))
                .RuleFor(u => u.Location, f => f.Lorem.Word())
                .RuleFor(u => u.Image, f => new PostImage()
                {
                    ImageBytes = ConvertImage.ToByteArray($"C:\\Programs\\Instagram\\Instagram\\GenerateFakeData\\FakePhotos\\{f.Random.Int(1, 20)}.jpg")
                })
                .RuleFor(u => u.PublicationDate, f => new DateTime(f.Random.Int(1950, 2024), f.Random.Int(1, 12), f.Random.Int(1, 28)))
                .RuleFor(u => u.Tags, GenerateFakeTags())
                .RuleFor(u => u.Comments, GenerateFakeComments());
            return fakePost.Generate();
        }

        private List<Post> GenerateFakePosts()
        {
            Random random = new Random();
            List<Post> posts = new List<Post>();
            for (int i = 0; i < random.Next(7); i++)
            {
                posts.Add(GenerateFakePost());
            }
            return posts;
        }

        private Tag GenerateFakeTag()
        {
            Faker<Tag> tag = new Faker<Tag>()
                .RuleFor(u => u.Text, f => f.Lorem.Word());
            return tag.Generate();
        }

        private List<Tag> GenerateFakeTags()
        {
            Random random = new Random();
            List<Tag> tags = new List<Tag>();
            for (int i = 0; i < random.Next(30); i++)
            {
                tags.Add(GenerateFakeTag());
            }
            return tags;
        }

        private Comment GenerateFakeComment()
        {
            Faker<Comment> fakeComment = new Faker<Comment>()
                .RuleFor(u => u.Content, f => f.Lorem.Paragraph(4))
                .RuleFor(u => u.PublicationDate, f => new DateTime(f.Random.Int(1950, 2024), f.Random.Int(1, 12), f.Random.Int(1, 28)))
                .RuleFor(u => u.CommentResponses, f => GenerateFakeCommentResponses())
                .RuleFor(u => u.AuthorId, f => f.Random.Int(1, 9) + _lastId);
            return fakeComment.Generate();
        }

        private List<Comment> GenerateFakeComments()
        {
            Random random = new Random();
            List<Comment> comments = new List<Comment>();
            for (int i = 0; i < random.Next(15); i++)
            {
                comments.Add(GenerateFakeComment());
            }
            return comments;
        }

        private CommentResponse GenerateFakeCommentResponse()
        {
            Faker<CommentResponse> fakeComment = new Faker<CommentResponse>()
                .RuleFor(u => u.Content, f => f.Lorem.Paragraph(4))
                .RuleFor(u => u.PublicationDate, f => new DateTime(f.Random.Int(1950, 2024), f.Random.Int(1, 12), f.Random.Int(1, 28)))
                .RuleFor(u => u.AuthorId, f => f.Random.Int(1, 9) + _lastId);
            return fakeComment.Generate();
        }

        private List<CommentResponse> GenerateFakeCommentResponses()
        {
            Random random = new Random();
            List<CommentResponse> comments = new List<CommentResponse>();
            for (int i = 0; i < random.Next(15); i++)
            {
                comments.Add(GenerateFakeCommentResponse());
            }
            return comments;
        }

        private Story GenerateFakeStory()
        {
            Faker<Story> fakeStory = new Faker<Story>()
                .RuleFor(u => u.Image, f => new StoryImage()
                {
                    ImageBytes = ConvertImage.ToByteArray($"C:\\Programs\\Instagram\\Instagram\\GenerateFakeData\\FakePhotos\\{f.Random.Int(1, 20)}.jpg")
                })
                .RuleFor(u => u.PublicationDate, f => new DateTime(f.Random.Int(2025, 2027), f.Random.Int(1, 12), f.Random.Int(1, 28)));
            return fakeStory.Generate();
        }

        private List<Story> GenerateFakeStories()
        {
            Random random = new Random();
            List<Story> stories = new List<Story>();
            for (int i = 0; i < random.Next(6); i++)
            {
                stories.Add(GenerateFakeStory());
            }
            return stories;
        }
    }
}
