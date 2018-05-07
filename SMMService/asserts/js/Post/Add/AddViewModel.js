var Post = Post || {};
Post.Add = Post.Add || {};

(function () {

    Post.Add.AddViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlPublicNow = theParams.UrlPublicNow;
        this.Post = new Post.PostModel(theParams.Post);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.Add.AddViewModel.prototype.constructor = Post.Add.AddViewModel;

    Post.Add.AddViewModel.prototype.PublicNow = function () {
        var self = this;
        var model = this.Post.GetData();
        console.log(model);
        $.post(this.UrlPublicNow, model).done(function (res) {
            if (res.IsSuccess) {
                window.location.href = res.Value;
            }
            else {
                console.log(res.Message);
            }
        });
    }
})();