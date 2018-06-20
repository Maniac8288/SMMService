var Archive = Archive || {};

(function () {

    Archive.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.RangeTime = ko.observable(theParams.RangeTime || "");
        this.Posts = ko.observableArray(theParams.Posts ? theParams.Posts.map(function (item) { return new Post.PostModel(item) }) : []);
        this.FilterPosts = ko.observableArray(this.Posts());
        this.FilterStatus = ko.observable("");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Archive.ViewModel.prototype.constructor = Archive.ViewModel;

    /**
     * Отфильтровать посты по статусы
     * @param {any} status статус
     */
    Archive.ViewModel.prototype.Filter = function (status) {
        if (status == "") {
            this.FilterPosts(this.Posts());
        }
        else {
            this.FilterPosts(this.Posts().filter(function (post) {
                return post.Status() == status;
            }));
        }
        this.FilterStatus(status);

    }
})();