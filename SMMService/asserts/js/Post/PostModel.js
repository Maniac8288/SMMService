var Post = Post || {};

(function () {

    Post.PostModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        this.Content = ko.observable(theParams.Content || "");
        this.DatePublic = ko.observable(theParams.DatePublic || "");
        this.DatePublicTime = ko.observable(theParams.DatePublicTime || "");
        this.Status = ko.observable(theParams.Status || "");
        this.ImageFile = ko.observable(theParams.ImageFile || "");
        this.ImagesUrl = ko.observableArray(theParams.ImagesUrl || "");
        this.Comments = ko.observableArray(theParams.Comments ? theParams.Comments.map(function (item) { return new Post.CommentModel(item) }) : []);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.PostModel.prototype.constructor = Post.PostModel;

    Post.PostModel.prototype.GetData = function () {
        return {
            Id: this.Id(),
            Name: this.Name(),
            Content: this.Content(),
            DatePublic: new Date(this.DatePublic() + " " + this.DatePublicTime()).toISOString(),
            ImageFile: this.ImageFile(),
        };
    }

    Post.PostModel.prototype.GetFormData = function () {
        var formData = new FormData();
        formData.append("Id", this.Id());
        formData.append("Name", this.Name());
        formData.append("Content", this.Content());
        formData.append("DatePublic", new Date(this.DatePublic() + " " + this.DatePublicTime()).toISOString());
        formData.append("ImageFile", this.ImageFile());
        return formData;
    }
})();