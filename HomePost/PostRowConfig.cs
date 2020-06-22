using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostRowConfig : MonoBehaviour
{
    public Text email, postTag, postTitle, postDescription, postDate;

    public void Initialise(Post post)
    {
        this.email.text = post.email;        
        this.postTag.text = post.postTag;
        this.postTitle.text = post.postTitle;
        this.postDescription.text = post.postDescription;
        this.postDate.text = post.postDate;
    }
}
