var Post = Post || {};

(function () {

    Post.VerificationViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlChangeContent = theParams.UrlChangeContent;
        this.UrlDeletePost = theParams.UrlDeletePost;
        this.UrlVerification = theParams.UrlVerification;
        this.ProjectId = theParams.ProjectId;
        this.Post = new Post.PostModel(theParams.Post);
        this.IsEditContent = ko.observable(false);
        this.TempContent = ko.observable(theParams.Post.Content);
        this.ErrorMessage = ko.observable("");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.VerificationViewModel.prototype.constructor = Post.VerificationViewModel;

    /**
     * Скопировать текст      
     **/
    Post.VerificationViewModel.prototype.Clipboard = function () {
        copyTextToClipboard(this.Post.Content());
    }
    /**
   * Сохранить от редактированный текст     
   **/
    Post.VerificationViewModel.prototype.EditContent = function () {
        var self = this;
        $.post(this.UrlChangeContent, { postId: this.Post.Id(), content: this.TempContent() }).done(function (response) {
            if (response.IsSuccess) {
                self.Post.Content(self.TempContent());
                self.IsEditContent(false);
            }
            else {
                self.ErrorMessage(response.Message);
            }
        }).fail(function (response) {
            console.error(response);
        });
    }
    /**
    * Отменить редактирование поста    
    **/
    Post.VerificationViewModel.prototype.CancelEdit = function () {
        this.IsEditContent(false);
        this.TempContent(this.Post.Content());

    }
    /**
   * Отменить редактирование поста    
   **/
    Post.VerificationViewModel.prototype.DeletePost = function () {
        $.post(this.UrlDeletePost, { postId: this.Post.Id(), projectId: this.ProjectId }).done(function (response) {
            if (response.IsSuccess) {
                window.location = response.Url;
            }
            else {
                self.ErrorMessage(response.Message);
            }
        }).fail(function (response) {
            console.error(response);
        });
    }
    Post.VerificationViewModel.prototype.Verification = function () {
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