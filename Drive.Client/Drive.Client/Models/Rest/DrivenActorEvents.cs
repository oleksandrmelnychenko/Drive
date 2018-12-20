namespace Drive.Client.Models.Rest {
    public enum DrivenActorEvents {
        /// <summary>
        /// Get new post.
        /// </summary>
        NewAnnounce = 0,
        /// <summary>
        /// Server event.
        /// </summary>
        ParseGovResponse = 1,
        /// <summary>
        /// Server event.
        /// </summary>
        CheckEmailBox = 2,
        /// <summary>
        /// Get all posts.
        /// </summary>
        GetAnnounces = 3,
        /// <summary>
        /// Comment post.
        /// </summary>
        CommentPost = 4,
        /// <summary>
        /// Post comments count.
        /// </summary>
        UpdatePostCommentsCount=5,
        /// <summary>
        /// Get post comments.
        /// </summary>
        GetPostComments = 6
    }
}
