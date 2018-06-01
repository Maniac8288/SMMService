var Post = Post || {};
Post.Add = Post.Add || {};

(function () {

    Post.Add.AddViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlPublicNow = theParams.UrlPublicNow;
        this.Post = new Post.PostModel(theParams.Post);
        this.Project = new Project.ProjectModel(theParams.Project)
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.Add.AddViewModel.prototype.constructor = Post.Add.AddViewModel;

    Post.Add.AddViewModel.prototype.PublicNow = function () {
        var self = this;
        var model = this.Post.GetData();
        model.ProjectId = this.Project.Id();
        model.Status = 1;
        $.post(this.UrlPublicNow, model).done(function (res) {
            if (res.IsSuccess) {
                window.location.href = res.Url;
            }
            else {
                console.log(res.Message);
            }
        });
    }
    Post.Add.AddViewModel.prototype.SchedulePublic = function () {
        var self = this;
        var model = this.Post.GetData();
        model.ProjectId = this.Project.Id();
        model.Status = 2;
        $.post(this.UrlPublicNow, model).done(function (res) {
            if (res.IsSuccess) {
                window.location.href = res.Url;
            }
            else {
                console.log(res.Message);
            }
        });
    }
})();