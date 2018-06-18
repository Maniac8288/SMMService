var Post = Post || {};

(function () {

    Post.PostViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlVerification = theParams.UrlVerification;
        this.UrlSendComment = theParams.UrlSendComment;
        this.Post = new Post.PostModel(theParams.Post);
        this.Comment = ko.observable("");
        this.StatusComment = ko.observable("All");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.PostViewModel.prototype.constructor = Post.PostViewModel;

    Post.PostViewModel.prototype.Verification = function () {
        var self = this;
        $.post(this.UrlVerification, { postId: this.Post.Id() }).done(function (res) {
            if (res.IsSuccess) {
                window.location.href = res.Url;
            }
            else {
                console.log(res.Message);
            }
        });
    }
    /**
     * Отправить комментарий 
     **/
    Post.PostViewModel.prototype.SendComment = function () {
        var self = this;
        if (this.Comment() == "")
            return false;
        $.post(this.UrlSendComment, { postId: this.Post.Id(), comment: this.Comment(), status: this.StatusComment() }).done(function (res) {
            if (res.IsSuccess) {
                self.Post.Comments.unshift(new Post.CommentModel(res.Value));
                self.Comment("");
            }
            else {
                console.error(res.Message);
            }
        });
    }
})();