var Post = Post || {};

(function () {

    Post.PostViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlVerification = theParams.UrlVerification;
        this.Post = new Post.PostModel(theParams.Post);
      
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
})();