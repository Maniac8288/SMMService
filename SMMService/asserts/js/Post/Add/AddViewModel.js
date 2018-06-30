var Post = Post || {};
Post.Add = Post.Add || {};

(function () {

    Post.Add.AddViewModel = function (theParams) {
        theParams = theParams || {};
        this.UrlPublicNow = theParams.UrlPublicNow;
        this.Post = ko.observable(new Post.PostModel(theParams.Post));
        this.Project = new Project.ProjectModel(theParams.Project)

        this.HashTagsSelected = ko.observableArray([]);        
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.Add.AddViewModel.prototype.constructor = Post.Add.AddViewModel;

    Post.Add.AddViewModel.prototype.Public = function (status) {
        var self = this;
        var fd = this.Post().GetFormData();
        fd.append("ProjectId", this.Project.Id());
        fd.append("Status", status);
        $.ajax({
            url: this.UrlPublicNow,
            data: fd,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (res) {
                if (res.IsSuccess) {
                    window.location.href = res.Url;
                }
                else {
                    console.log(res.Message);
                }
            }
        });     
    }

    Post.Add.AddViewModel.prototype.fileUpload = function (data, e) {
        var self = this;
        this.Post().ImageFile(e.target.files[0]);
    };
    Post.Add.AddViewModel.prototype.VideoUpload = function (data, e) {
        var self = this;
        this.Post().VideoFile(e.target.files[0]);
    };
    Post.Add.AddViewModel.prototype.AddHashtag = function (hashTag) {
        this.HashTagsSelected.push(hashTag);
        this.Post().Content(this.Post().Content() + " " + hashTag.Title() + " ")
        $(".emojionearea-editor").html(this.Post().Content());
    }
})();