var Post = Post || {};

(function () {

    Post.CommentModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Content = ko.observable(theParams.Content || "");
        this.DateCreate = ko.observable(theParams.DateCreate || "");      
        this.User = ko.observable(new User.UserModel(theParams.User || {}));
        return this;
    };

    /**
    * Определяем конструктор
    */
   Post.CommentModel.prototype.constructor =Post.CommentModel;

 

 
})();