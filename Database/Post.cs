using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post
{
    public string email, imageUrl, postTag, postTitle, postDescription, postDate;

    public Post()
    {
        email = UserManager.email;
        imageUrl = PostManager.imageUrl;        
        postTag = PostManager.postTag;
        postTitle = PostManager.postTitle;
        postDescription = PostManager.postDescription;
        postDate = PostManager.postDate;
    }

    public Post(IDictionary<string, object> dict)
    {
        this.email = dict["email"].ToString();
        this.imageUrl = dict["imageUrl"].ToString();
        this.postTag = dict["postTag"].ToString();
        this.postTitle = dict["postTitle"].ToString();
        this.postDescription = dict["postDescription"].ToString();
        this.postDate = dict["postDate"].ToString();
    }
}
